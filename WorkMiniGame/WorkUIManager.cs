using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WorkUIManager : MonoBehaviour
{
    public int money { get; set; } = 0;

    private int C_INIT_MONEY = 0;

    private bool isStampBookCalled = false;
    private bool isAuthorListCalled = false;
    private bool isAcademyListCalled = false;
    private bool isInstitutonListCalled = false;

    [SerializeField]
    private TMP_Text timer;

    [SerializeField]
    private GameObject GameOverUI;

    [SerializeField]
    private GameObject warningPopup;

    [SerializeField]
    private RectTransform paperGenPosition;

    [SerializeField]
    private RectTransform listPaperInitPos;

    [SerializeField]
    private WorkManager workManager;

    [SerializeField]
    private Canvas canvas;

    [SerializeField]
    private TextMeshProUGUI wageText;

    [SerializeField]
    private Button AuthorButton;

    [SerializeField]
    private Button AcademyButton;

    [SerializeField]
    private Button InstitutonButton;

    [SerializeField]
    private GameObject stampBook;

    [SerializeField]
    private Button StampBookButton;

    void Awake()
    {
        
    }
    public void StartPartTime()
    {
        UpdateMoney(C_INIT_MONEY);
        workManager.InitPartTime();
        SetNextPaper();
        UpdateTimer();
    }
    public void SetNextPaper()
    {
        workManager.GenPaper(paperGenPosition);
    }

    public void CallStampBook()
    {
        isStampBookCalled = !isStampBookCalled;
        stampBook.SetActive(isStampBookCalled);
    }
    public void CallAuthorList()
    {
        isAuthorListCalled = !isAuthorListCalled;
        workManager.GetAuthorList().SetActive(isAuthorListCalled);
    }
    public void CallAcademyList()
    {
        isAcademyListCalled = !isAcademyListCalled;
        workManager.GetAcademyList().SetActive(isAcademyListCalled);
    }
    public void CallInstitutonList()
    {
        isInstitutonListCalled = !isInstitutonListCalled;
        workManager.GetInstitutonList().SetActive(isInstitutonListCalled);
    }

    public bool GameOver()
    {
        return workManager.IsGameOver();
    }

    public void DestroyMonitorPaper()
    {
        workManager.DestroyPaper();
    }

    public IEnumerator ShowGameOverUI()
    {
        workManager.DestroyPaper();

        GameOverUI.SetActive(true);

        yield return StartCoroutine(GameOverUI.GetComponent<PartTimeResult>().PrintFinalWage(workManager.wage));
    }
    
    public void UpdateMoney(int pay)
    {
        workManager.wage += pay;
        wageText.text = "Wage : " + workManager.wage;
    }

    public void UpdateTimer()
    {
        timer.text = "Remaining time : " + workManager.timer.GetlimitTime();
    }

    public void PrintWarning()
    {
        warningPopup.SetActive(true);
        warningPopup.transform.SetAsLastSibling();
    }
    
    public void ShutdownPartTime()
    {
        StartCoroutine(ShowGameOverUI());
    }

}
