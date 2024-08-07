using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

using static TURN;
using static ACTION;

public class UIManager : MonoBehaviour
{
    private float playerPoint;
    private float AIPoint;
    private bool toggleDecisionButton = false;
    private bool warningFlag = false;

    private const int C_START_CARD_NUMBER = 2;

    private WaitForSeconds delay = new WaitForSeconds(0.5f);

    public ExtendPRS AIStartPos { get; set; }

    public ExtendPRS playerStartPos { get; set; }

    public ExtendPRS AIEndPos { get; set; }

    public ExtendPRS playerEndPos { get; set; }

    [SerializeField]
    private Button hit;

    [SerializeField]
    private Button stand;

    [SerializeField]
    private GameObject blackJackUI;

    [SerializeField]
    private CardManager cardManager;

    [SerializeField]
    private TextMeshProUGUI playerSum;

    [SerializeField]
    private TextMeshProUGUI AISum;

    [SerializeField]
    private TextMeshProUGUI systemText;

    [SerializeField]
    private TextMeshProUGUI AIDecisionText;

    [SerializeField]
    private TextMeshProUGUI winnerText;

    [SerializeField]
    private Transform AIStartPosition;

    [SerializeField]
    private Transform playerStartPosition;

    [SerializeField]
    private Transform AIEndPosition;

    [SerializeField]
    private Transform playerEndPosition;

    [SerializeField]
    private BlackJackManager blackJackManager;

    [SerializeField]
    private GameObject GameOver;

    [SerializeField]
    private GameObject decisionButton;

    [SerializeField]
    private GameObject closeButton;

    [SerializeField]
    private GameObject warningPopup;

    void Awake()
    {
        //button
        hit.onClick.AddListener(() => PlayerDoHit());
        stand.onClick.AddListener(() => PlayerDoStand());

        systemText.gameObject.SetActive(false);

        InitCardPos();
    }

    public void PrintWarning()
    {
        /*if (warningFlag)
            return;
        else
        {*/
            warningFlag = true;
            warningPopup.SetActive(true);
            blackJackManager.ToggleActiveCards(false);
            
        //}
    }

    public void ShutdownGame()
    {
        warningPopup.SetActive(false);

        //강제 종료시 일단 해당 턴은 "AI승리" 로 설정하고 해당 턴까지 얻은 학습 포인트까지 반영시켰음.
        blackJackManager.UpdateStudyPoint(AI);

        StartCoroutine(ShowGameOver());
    }

    public void ContinueGame()
    {
        warningFlag = false;
        warningPopup.SetActive(false);
        blackJackManager.ToggleActiveCards(true);
    }

    private void InitCardPos()
    {
        playerStartPos = new ExtendPRS(playerStartPosition.position, playerStartPosition.rotation, playerStartPosition.localScale);
        playerEndPos = new ExtendPRS(playerEndPosition.position, playerEndPosition.rotation, playerEndPosition.localScale);

        playerPoint = playerStartPos.pos.x;

        AIStartPos = new ExtendPRS(AIStartPosition.position, AIStartPosition.rotation, AIStartPosition.localScale);
        AIEndPos = new ExtendPRS(AIEndPosition.position, AIEndPosition.rotation, AIEndPosition.localScale);

        AIPoint = AIStartPos.pos.x;
    }

    private IEnumerator ToggleDecisionButton()
    {
        toggleDecisionButton = !toggleDecisionButton;
        decisionButton.SetActive(toggleDecisionButton);
        yield return null;
    }

    public IEnumerator DoBehavior()
    {
        yield return StartCoroutine(AIDoAction());

        yield return StartCoroutine(UpdateUI());

        if (blackJackManager.GetPlayerAction() == STAND && blackJackManager.GetAIAction() == STAND)
        {
            yield return StartCoroutine(SetWinner());
            blackJackManager.ReGame();
            UIinit();
        }
        else
        {
            yield return StartCoroutine(ToggleDecisionButton());
        }

    }

    public IEnumerator AIDoAction()
    {
        if (blackJackManager.AIDecisionAction())
        {
            if (blackJackManager.IsGameOver())
            {
                yield return StartCoroutine(SetWinner());
                yield return StartCoroutine(ShowGameOver());
            }
            else
            {
                yield return StartCoroutine(DoHit());

                AIDecisionText.text = "AI Hit!!";
            }
        }
        else
        {
            yield return StartCoroutine(DoStand());

            AIDecisionText.text = "AI Stand!!";
        }
    }

    public void PlayerDoHit()
    {
        StartCoroutine(ProgressTurn(HIT));
    }

    public void PlayerDoStand()
    {
        StartCoroutine(ProgressTurn(STAND));
    }

    public IEnumerator ProgressTurn(ACTION action)
    {
        yield return StartCoroutine(ToggleDecisionButton());

        if (action == HIT)
            yield return StartCoroutine(DoHit());

        else if (action == STAND)
            yield return StartCoroutine(DoStand());

        yield return StartCoroutine(DoBehavior());
    }

    public IEnumerator DoHit()
    {
        Debug.Log(blackJackManager.GetWhosTurn() + " hit");

        if (blackJackManager.IsGameOver())
        {
            yield return StartCoroutine(SetWinner());
            yield return StartCoroutine(ShowGameOver());
        }
        else
        {
            TURN turn = blackJackManager.GetWhosTurn();

            blackJackManager.OffTargetFlip(turn);
            blackJackManager.Hit();

            List<GameObject> cards = turn == PLAYER ? cardManager.playerCards : cardManager.AICards;

            FlipCard(cards);

            yield return StartCoroutine(SortCard(turn));
        }

    }

    public IEnumerator DoStand()
    {
        if (cardManager.AICards.Count == 2 && blackJackManager.GetWhosTurn() == AI)
            FlipCard(cardManager.AICards);

        if (cardManager.playerCards.Count == 2 && blackJackManager.GetWhosTurn() == PLAYER)
            FlipCard(cardManager.playerCards);

        blackJackManager.Stand();

        yield return delay;
    }

    private IEnumerator ShowGameOver()
    {
        systemText.gameObject.SetActive(false);
        GameOver.SetActive(true);
        GameOver.GetComponent<Result>().PrintResult(blackJackManager.studyPoint);

        toggleDecisionButton = false;
        blackJackUI.SetActive(false);

        yield return null;
    }

    public IEnumerator BlackJack()
    {
        var aiCards = cardManager.AICards;
        var playerCards = cardManager.playerCards;

        //draw
        if (blackJackManager.IsBlackJack(playerCards) && blackJackManager.IsBlackJack(aiCards))
        {
            systemText.gameObject.SetActive(true);
            systemText.text = "Draw!!";
            Invoke("OffSystemMessage", 1.5f);
        }
        else if (blackJackManager.IsBlackJack(playerCards))
        {
            systemText.gameObject.SetActive(true);
            systemText.text = "Player is BlackJack!!, Winner winner chicken dinner!!";
            Invoke("OffSystemMessage", 1.5f);
        }
        else if (blackJackManager.IsBlackJack(playerCards))
        {
            systemText.gameObject.SetActive(true);
            systemText.text = "AI is BlackJack!!, Winner winner chicken dinner!!";
            Invoke("OffSystemMessage", 1.5f);
        }

        FlipCard(aiCards);
        yield return delay;

        FlipCard(playerCards);
        yield return new WaitForSeconds(2.0f);
    }

    public IEnumerator SetWinner()
    {
        yield return StartCoroutine(WhoWinner());
    }

    public IEnumerator WhoWinner()
    {
        systemText.gameObject.SetActive(true);

        TURN winner = blackJackManager.FindWinner();

        if (winner == PLAYER)
        {
            systemText.text = "Player WIN!!";
        }
        else if (winner == AI)
        {
            systemText.text = "AI WIN!";
        }
        else
        {
            systemText.text = "Draw!";
        }

        blackJackManager.UpdateStudyPoint(winner);

        yield return new WaitForSeconds(2.0f);

        systemText.gameObject.SetActive(false);
    }

    public void FlipCard(List<GameObject> cards)
    {
        if (cards.Count > 3)
            return;

        foreach (GameObject card in cards)
        {
            var targetCard = card.GetComponent<Card>();

            if (targetCard.isFront)
                continue;
            else
                targetCard.Rotate();
        }
    }

    public IEnumerator Burst(TURN burstInfo)
    {
        systemText.gameObject.SetActive(true);

        switch (burstInfo)
        {
            case PLAYER:
                systemText.text = "Player Brust!!";
                break;

            case AI:
                systemText.text = "AI Brust!!";
                break;

            default:
                systemText.text = "Draw!!";
                break;
        }

        blackJackManager.UpdateStudyPoint(burstInfo);

        yield return new WaitForSeconds(2.0f);

        systemText.gameObject.SetActive(false);

        blackJackManager.ReGame();

        StopAllCoroutines();

        UIinit();
    }

    public void UIinit()
    {
        if (blackJackManager.IsGameOver())
        {
            //systemText.gameObject.SetActive(true);
            //systemText.text = "GameOver!!";
            //Invoke("OffSystemMessage", 1.5f);
            StopAllCoroutines();
            StartCoroutine(PrintGameOver());
        }
        else
        {
            blackJackManager.OrderStartCard();
            StartCoroutine(SortStartCard());
            UpdatePlayerSum();
            InitAISum();
            BlackJack();
        }
    }

    private IEnumerator PrintGameOver()
    {
        yield return StartCoroutine(GameOverText());
        yield return StartCoroutine(ShowGameOver());
    }

    private IEnumerator GameOverText()
    {
        systemText.gameObject.SetActive(true);
        systemText.text = "GameOver!!";
        yield return new WaitForSeconds(1.5f);
    }

    public IEnumerator UpdateUI()
    {
        UpdatePlayerSum();
        UpdateAISum();

        warningFlag = false;

        if (blackJackManager.IsBurst())
        {
            yield return StartCoroutine(Burst(blackJackManager.GetBurstInfo()));
        }
    }

    public IEnumerator SortStartCard()
    {
        yield return StartCoroutine(SortCard(PLAYER));
        yield return StartCoroutine(SortCard(AI));
        yield return StartCoroutine(ToggleDecisionButton());
    }

    private void UpdatePlayerSum()
    {
        playerSum.text = "Player = " + blackJackManager.GetPlayerSum();
    }

    private void UpdateAISum()
    {
        AISum.text = "AI = " + blackJackManager.GetAICardSum();
    }

    private void InitAISum()
    {
        AISum.text = "AI = " + blackJackManager.GetAIUpCardSum();
    }

    public IEnumerator SortCard(TURN turn)
    {
        var gamerInfo = FindGamerInfo(turn);

        List<GameObject> cards = gamerInfo.Item1;
        Tuple<ExtendPRS, ExtendPRS> posInfo = gamerInfo.Item2;

        for (int idx = 1; idx <= cards.Count; ++idx)
        {
            yield return delay;
            cards[idx - 1].GetComponent<Card>().MoveTransform(GetCardPos(posInfo.Item1, posInfo.Item2, cards.Count + 1, idx), 0.7f);
        }
    }

    public Tuple<List<GameObject>, Tuple<ExtendPRS, ExtendPRS>> FindGamerInfo(TURN turn)
    {
        List<GameObject> cards;
        Tuple<ExtendPRS, ExtendPRS> pos;

        if (turn == PLAYER)
        {
            cards = cardManager.playerCards;
            pos = new Tuple<ExtendPRS, ExtendPRS>(playerStartPos, playerEndPos);
        }
        else
        {
            cards = cardManager.AICards;
            pos = new Tuple<ExtendPRS, ExtendPRS>(AIStartPos, AIEndPos);
        }

        return new Tuple<List<GameObject>, Tuple<ExtendPRS, ExtendPRS>>(cards, pos);
    }

    public ExtendPRS GetCardPos(ExtendPRS start, ExtendPRS end, float cardNum, float count)
    {
        var x = LinearInterpolation(start.pos.x, end.pos.x, (float)(count / cardNum));
        return new ExtendPRS(new Vector3(x, start.pos.y, start.pos.z), start.rot, start.scale);
    }

    private float LinearInterpolation(float p1, float p2, float d1)
    {
        return (1 - d1) * p1 + d1 * p2;
    }

}
