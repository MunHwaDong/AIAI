using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

using static TYPE;

public class XMLManager : MonoBehaviour
{

    [SerializeField]
    private int validAuthorRange;

    [SerializeField]
    private int validAcademyRange;

    [SerializeField]
    private int validInstitutionRange;

    public List<Thesis> LoadThesisXML()
    {
        //데이터를 형성할 문서 생성 및 파일읽기
        string filepath = Application.streamingAssetsPath + "/Datas/thesis.xml";

        XmlDocument doc = new XmlDocument();

        List<Thesis> thesisDatas = new();

        doc.Load(filepath);

        //루트 설정
        XmlElement nodes = doc["ThesisList"];

        //루트에서 요소 꺼내기
        foreach (XmlElement node in nodes.ChildNodes)
        {
            string title = node.GetAttribute("title");
            string academy = node.GetAttribute("academy");
            string author = node.GetAttribute("author");
            bool isValid = System.Convert.ToBoolean(node.GetAttribute("isValid"));

            Thesis readData = new Thesis(title, academy, author, isValid, THESIS);

            //가져온 데이터를 리스트에 입력
            thesisDatas.Add(readData);
        }

        return thesisDatas;

    }

    public List<Document> LoadDocuXML()
    {
        //데이터를 형성할 문서 생성 및 파일읽기
        string filepath = Application.streamingAssetsPath + "/Datas/document.xml";

        XmlDocument doc = new XmlDocument();

        List<Document> docuDatas = new();

        doc.Load(filepath);

        //루트 설정
        XmlElement nodes = doc["DocumentList"];

        //루트에서 요소 꺼내기
        foreach (XmlElement node in nodes.ChildNodes)
        {
            string title = node.GetAttribute("title");
            string institution = node.GetAttribute("institution");
            string stampFileName = node.GetAttribute("stampFileName");
            bool isValid = System.Convert.ToBoolean(node.GetAttribute("isValid"));

            Document readData = new Document(title, institution, stampFileName, isValid, DOCUMENT);

            //가져온 데이터를 리스트에 입력
            docuDatas.Add(readData);
        }

        return docuDatas;

    }

    public List<string> MakeAuthorListData(List<Paper> papers)
    {
        List<string> authors = new List<string>();

        Thesis paperList = new Thesis();

        for(int idx = 0; idx < validAuthorRange; ++idx)
        {
            paperList = papers[idx] as Thesis;
            authors.Add(paperList.author);
        }

        return authors;

    }

    public List<string> MakeAcademyListData(List<Paper> papers)
    {
        List<string> academys = new List<string>();

        Thesis paperList = new Thesis();

        for (int idx = 0; idx < validAcademyRange; ++idx)
        {
            paperList = papers[idx] as Thesis;
            academys.Add(paperList.academy);
        }

        return academys;

    }

    public List<string> MakeInstitutionListData(List<Paper> papers)
    {
        List<string> institution = new List<string>();

        Document paperList = new Document();

        for (int idx = 0; idx < validInstitutionRange; ++idx)
        {
            paperList = papers[idx] as Document;
            institution.Add(paperList.institution);
        }

        return institution;

    }

}