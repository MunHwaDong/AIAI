using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Strategy
{

    public int AITotal { get; set; }
    public int PlayerUpCard { get; set; }

    // <action, prob>
    // stand/hit table, 각 셀에 맞는 행동을 진짜로 할 지 확률.
    // 행 = 내 카드, 열 = 상대 카드
    // 난이도별로 주는 지능 수치를 다르게해서 변수로 저장하고 있자.
    public abstract bool Thinking();
}

public class BasicStrategy : Strategy
{
    public override bool Thinking()
    {
        return ShouldHit(AITotal, PlayerUpCard);
    }

    public bool ShouldHit(int aiTotal, int playerUpCard)
    {
        // 플레이어의 손이 소프트 17인 경우
        if (aiTotal == 17 && HasAce(aiTotal))
        {
            return true;
        }

        // 딜러의 업 카드가 7 이상인 경우
        if (playerUpCard >= 7)
        {
            if (aiTotal <= 11)
            {
                return true;
            }
            if (aiTotal == 12 && (playerUpCard == 2 || playerUpCard == 3))
            {
                return true;
            }
            if (aiTotal <= 15 && playerUpCard == 10)
            {
                return true;
            }
            if (aiTotal <= 16 && playerUpCard == 9)
            {
                return true;
            }
        }

        // 딜러의 업 카드가 4, 5, 6인 경우
        if (playerUpCard >= 4 && playerUpCard <= 6)
        {
            if (aiTotal <= 11)
            {
                return true;
            }
            if (aiTotal == 12 && (playerUpCard == 2 || playerUpCard == 3))
            {
                return true;
            }
            if (aiTotal <= 15 && (playerUpCard == 10 || playerUpCard == 11))
            {
                return true;
            }
            if (aiTotal <= 16 && playerUpCard == 9)
            {
                return true;
            }
        }

        // 그 외의 경우
        if (aiTotal <= 11)
        {
            return true;
        }
        if (aiTotal == 12 && playerUpCard <= 3)
        {
            return true;
        }
        if (aiTotal <= 16 && playerUpCard >= 7)
        {
            return true;
        }

        return false;
    }

    public bool HasAce(int total)
    {
        foreach (var card in Hand(total))
        {
            if (card == 1)
            {
                return true;
            }
        }
        return false;
    }

    public IEnumerable<int> Hand(int total)
    {
        // 손에 있는 각 카드의 값을 반환하는 함수
        // (A는 1 또는 11로 계산할 수 있으므로 리스트로 반환)
        if (total <= 11)
        {
            yield return 1;
            yield return total - 1;
        }
        else if (total == 12)
        {
            yield return 1;
            yield return 11;
        }
        else
        {
            yield return total - 10;
        }
    }
}

public class BeginnerCardCounting : Strategy
{
    private int count;
    public override bool Thinking()
    {
        return true;
    }

    public void UpdateCount(Card card)
    {
        if (card.CardNumber >= 2 && card.CardNumber <= 6) // 2, 3, 4, 5, 6 카드가 나왔을 때
            count++;
        else if (card.CardNumber >= 10 || card.CardNumber == 1) // 10, J, Q, K, A 카드가 나왔을 때
            count--;
    }

    public int GetCount()
    {
        return count;
    }

    public void ResetCount()
    {
        count = 0;
    }
}
