using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WorkManager : MonoBehaviour
{

    private GameObject onTablePaperObj;
    private GameObject onTableIconObj;

    public Timer timer;

    public int wage { get; set; } = 0;

    [SerializeField]
    private float initTime;

    [SerializeField]
    private PaperManager paperManager;

    [SerializeField]
    private XMLManager DataManager;

    [SerializeField]
    private GameObject ThesisZone;

    [SerializeField]
    private GameObject DocumentZone;

    [SerializeField]
    private GameObject TreshZone;

    void Awake()
    {
        timer = new Timer(initTime);
    }

    public void InitPartTime()
    {
        timer = new Timer(initTime);
        paperManager.ToggleRightMonitor();
        paperManager.InitPapers();
    }

    public void ResetPartTime()
    {
        timer = null;
        wage = 0;
        paperManager.ToggleRightMonitor();
        paperManager.DestroyPapers();
        GC.Collect();
    }

    public void EndGame()
    {
        //데이터 매니저 연동 및 이후 게임 진행.
        //WorkManager(this)의 wage변수를 인자로 넘기면 될 것 같습니다
        ResetPartTime();
    }

    public GameObject GetAuthorList()
    {
        return paperManager.authorList;
    }

    public GameObject GetAcademyList()
    {
        return paperManager.academyList;
    }
    public GameObject GetInstitutonList()
    {
        return paperManager.institutonList;
    }

    public void GenPaper(RectTransform genPos)
    {
        onTablePaperObj = paperManager.GetPaperObj(genPos);

        //onTablePaperObj.GetComponent<Paper>().MoveTransform(genPos, 0.7f);
    }

    public bool IsGameOver()
    {
        if (timer == null || timer.GetTime() <= 0)
        {
            return true;
        }
        else
            return false;
    }

    public void DestroyPaper()
    {
        Destroy(onTablePaperObj);
    }

}
