using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

using static TYPE;

public class PaperManager : MonoBehaviour
{
    private bool toggleRightMonitor = false;

    private Stack<Paper> papers = new();

    public GameObject authorList { get; private set; }
    public GameObject academyList { get; private set; }
    public GameObject institutonList { get; private set; }

    public List<Paper> theses { get; private set; }

    public List<Paper> documents { get; private set; }

    //참고 리스트.
    private List<string> authorInfo;
    private List<string> academyInfo;
    private List<string> institutionInfo;

    [SerializeField]
    private GameObject desk;

    [SerializeField]
    private Canvas leftMonitor;

    [SerializeField]
    private Canvas rightMonitor;

    [SerializeField]
    private XMLManager DataManager;

    [SerializeField]
    private GameObject ThesisPrefabs;

    [SerializeField]
    private GameObject DocumentPrefabs;

    [SerializeField]
    private GameObject ListPaperPrefabs;

    public GameObject PaperIcon;

    public GameObject IconCanvas;

    void Awake()
    {
        InitPapers();
    }

    public void InitPapers()
    {
        LoadThesisData();
        LoadDocuData();
        MakePaperStack(theses);
        MakePaperStack(documents);
        Shuffle();
        MakeAuthorList();
        MakeAcademyList();
        MakeInstitutonList();
    }

    public void DestroyPapers()
    {
        papers.Clear();
        authorInfo.Clear();
        academyInfo.Clear();
        institutionInfo.Clear();
        theses.Clear();
        documents.Clear();
        Destroy(authorList);
        Destroy(institutonList);
        Destroy(academyList);
        GC.Collect();
    }

    public void ToggleRightMonitor()
    {
        rightMonitor.gameObject.SetActive(!toggleRightMonitor);
    }


    private void LoadThesisData()
    {
        theses = new List<Paper>(DataManager.LoadThesisXML());
        authorInfo = DataManager.MakeAuthorListData(theses);
        academyInfo = DataManager.MakeAcademyListData(theses);
    }

    private void LoadDocuData()
    {
        documents = new List<Paper>(DataManager.LoadDocuXML());
        institutionInfo = DataManager.MakeInstitutionListData(documents);
    }

    private void MakePaperStack(List<Paper> paperList)
    {
        foreach(Paper paper in paperList)
        {
            papers.Push(paper);
        }
    }

    private void MakeAuthorList()
    {
        AuthorList authorL = new AuthorList(authorInfo);

        GameObject authors = Instantiate(ListPaperPrefabs, leftMonitor.transform.position, Quaternion.identity);
        authors.SetActive(false);

        authors.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "저자명";
        authors.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = authorL.MakePaper();

        authors.transform.SetParent(leftMonitor.transform);

        authors.GetComponent<Draggable>().canvas = leftMonitor;

        authors.transform.localScale = new Vector3(4f, 4f, 4f);

        authorList = authors;
        authorList.AddComponent<AuthorList>();
    }

    private void MakeAcademyList()
    {
        AcademyList academyL = new AcademyList(academyInfo);

        GameObject academys = Instantiate(ListPaperPrefabs, leftMonitor.transform.position, Quaternion.identity);
        academys.SetActive(false);

        academys.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "학회명";
        academys.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = academyL.MakePaper();

        academys.transform.SetParent(leftMonitor.transform);

        academys.GetComponent<Draggable>().canvas = leftMonitor;

        academys.transform.localScale = new Vector3(4f, 4f, 4f);

        academyList = academys;
        academyList.AddComponent<AcademyList>();
    }

    private void MakeInstitutonList()
    {
        InstitutonList institutonL = new InstitutonList(institutionInfo);

        GameObject institutons = Instantiate(ListPaperPrefabs, leftMonitor.transform.position, Quaternion.identity);
        institutons.SetActive(false);

        institutons.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "기관 및 단체명";
        institutons.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = institutonL.MakePaper();

        institutons.transform.SetParent(leftMonitor.transform);

        institutons.GetComponent<Draggable>().canvas = leftMonitor;

        institutons.transform.localScale = new Vector3(4f, 4f, 4f);

        institutonList = institutons;
        institutons.AddComponent<InstitutonList>();
    }

    private void Shuffle()
    {

        List<Paper> shuffleSpace = new List<Paper>(papers);

        System.Random random = new System.Random();

        for (int i = shuffleSpace.Count - 1; i > 0; i--)
        {

            int j = random.Next(i + 1);

            Paper tmp = shuffleSpace[i];

            shuffleSpace[i] = shuffleSpace[j];

            shuffleSpace[j] = tmp;

        }

        papers = new Stack<Paper>(shuffleSpace);

    }

    public GameObject GetPaperObj(RectTransform genPos)
    {
        Paper paper = GetPaper();

        GameObject iconObj = GenIcon();

        GameObject paperObj ;

        if (paper is Thesis)
        {
            Thesis convPaper = paper as Thesis;
            paperObj = Instantiate(ThesisPrefabs, genPos.position, Quaternion.identity);
            paperObj.GetComponent<Paper>().isValidPaper = paper.isValidPaper;
            paperObj.GetComponent<Paper>().paperSprite = paper.paperSprite;

            paperObj.GetComponent<Paper>().paperType = THESIS;
            iconObj.GetComponent<Icon>().iconType = THESIS;

            paperObj.GetComponent<Thesis>().author = convPaper.author;
            paperObj.GetComponent<Thesis>().academy = convPaper.academy;

            //title 설정
            paperObj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = convPaper.title;

            //author & academy 설정
            paperObj.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = convPaper.author + " : " + convPaper.academy;
        }
        else if (paper is Document)
        {
            Document convPaper = paper as Document;
            paperObj = Instantiate(DocumentPrefabs, genPos.position, Quaternion.identity);
            paperObj.GetComponent<Paper>().isValidPaper = paper.isValidPaper;
            paperObj.GetComponent<Paper>().paperSprite = paper.paperSprite;

            paperObj.GetComponent<Paper>().paperType = DOCUMENT;
            iconObj.GetComponent<Icon>().iconType = DOCUMENT;

            paperObj.GetComponent<Document>().institution = convPaper.institution;
            paperObj.GetComponent<Document>().stampFilePath = convPaper.stampFilePath;

            //title 설정
            paperObj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = convPaper.title;

            //institution 설정
            paperObj.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = convPaper.institution;

            //도장 스프라이트 설정
            paperObj.transform.GetChild(2).GetComponent<Image>().sprite = Resources.Load<Sprite>(convPaper.stampFilePath);

        }
        else
        {
            paperObj = Instantiate(DocumentPrefabs, genPos.position, Quaternion.identity);
            paperObj.GetComponent<Paper>().paperType = TRASH;
            iconObj.GetComponent<Icon>().iconType = TRASH;
        }
        //유효 문서가 아닐때 문서 타입을 쓰레기 문서로 만들어준다.
        if (!paper.isValidPaper)
        {
            Debug.Log("Trash");
            paperObj = Instantiate(DocumentPrefabs, genPos.position, Quaternion.identity);
            paperObj.GetComponent<Paper>().paperType = TRASH;
            iconObj.GetComponent<Icon>().iconType = TRASH;
        }
        
        paperObj.transform.SetParent(rightMonitor.transform);

        //paperObj.GetComponent<Draggable>().canvas = rightMonitor;

        paperObj.transform.localScale = new Vector3(1f, 1f, 1f);

        iconObj.GetComponent<Draggable>().canvas = leftMonitor;
        iconObj.GetComponent<DoubleClick>().paper = paperObj.GetComponent<Paper>();

        paperObj.GetComponent<DoubleClick>().paper = paperObj.GetComponent<Paper>();
        paperObj.GetComponent<Paper>().iconObj = iconObj;
        paperObj.GetComponent<Paper>().paperObj = paperObj;

        return paperObj;
    }

    private GameObject GenIcon()
    {
        GameObject icon = Instantiate(PaperIcon, IconCanvas.transform.position, Quaternion.identity);
      
        icon.SetActive(false);
        icon.transform.SetParent(IconCanvas.transform);
        icon.transform.localScale = new Vector3(1f, 1f, 1f);

        return icon;
    }
    public void DestroyIcon()
    {
        foreach (Transform child in IconCanvas.GetComponent<RectTransform>())
        {
            Destroy(child.gameObject);
        }
    }
    private Paper GetPaper()
    {
        return papers.Pop();
    }

}
