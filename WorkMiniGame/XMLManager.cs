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
        //�����͸� ������ ���� ���� �� �����б�
        string filepath = Application.streamingAssetsPath + "/Datas/thesis.xml";

        XmlDocument doc = new XmlDocument();

        List<Thesis> thesisDatas = new();

        doc.Load(filepath);

        //��Ʈ ����
        XmlElement nodes = doc["ThesisList"];

        //��Ʈ���� ��� ������
        foreach (XmlElement node in nodes.ChildNodes)
        {
            string title = node.GetAttribute("title");
            string academy = node.GetAttribute("academy");
            string author = node.GetAttribute("author");
            bool isValid = System.Convert.ToBoolean(node.GetAttribute("isValid"));

            Thesis readData = new Thesis(title, academy, author, isValid, THESIS);

            //������ �����͸� ����Ʈ�� �Է�
            thesisDatas.Add(readData);
        }

        return thesisDatas;

    }

    public List<Document> LoadDocuXML()
    {
        //�����͸� ������ ���� ���� �� �����б�
        string filepath = Application.streamingAssetsPath + "/Datas/document.xml";

        XmlDocument doc = new XmlDocument();

        List<Document> docuDatas = new();

        doc.Load(filepath);

        //��Ʈ ����
        XmlElement nodes = doc["DocumentList"];

        //��Ʈ���� ��� ������
        foreach (XmlElement node in nodes.ChildNodes)
        {
            string title = node.GetAttribute("title");
            string institution = node.GetAttribute("institution");
            string stampFileName = node.GetAttribute("stampFileName");
            bool isValid = System.Convert.ToBoolean(node.GetAttribute("isValid"));

            Document readData = new Document(title, institution, stampFileName, isValid, DOCUMENT);

            //������ �����͸� ����Ʈ�� �Է�
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