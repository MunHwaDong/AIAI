using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public enum LISTTYPE { AUTHOR, ACADEMY, INSTITUTION, STAMP };

public abstract class ListPaper : MonoBehaviour
{
    public LISTTYPE ListType;

    public GameObject listPaper;

    public PaperManager paperManager;

    public void MoveTransform(Transform pos, float dotweenTime = 0f)
    {
        transform.DOMove(pos.position, dotweenTime);
        transform.DORotateQuaternion(pos.rotation, dotweenTime);
    }

    public IEnumerator ListPaperAnimation()
    {
        RectTransform thisRect = (RectTransform)gameObject.transform;
        thisRect.sizeDelta = new Vector2(0, 0);
        float time = 0.0f;
        while (time < 0.2)
        {
            thisRect.sizeDelta = new Vector2(31 * (time * 10), 49 * (time * 10));
            time = time + 0.02f;
            yield return new WaitForSeconds(0.02f);
        }
        thisRect.sizeDelta = new Vector2(62, 98);
        yield return null;
    }

    public abstract string MakePaper();

    public abstract void LoadData(string data);

}


public class AuthorList : ListPaper
{
    List<string> authorList = new();
    public AuthorList(List<string> data)
    {
        this.paperManager = GameObject.Find("PaperManager").GetComponent<PaperManager>();
        this.authorList = data;
        //LoadData(data);
        this.ListType = LISTTYPE.AUTHOR;
    }

    public override string MakePaper()
    {
        string authors = "";

        foreach (string author in authorList)
        {
            authors += author + "\n";
        }

        return authors;
    }

    public override void LoadData(string data)
    {
        List<Paper> thesisDatas = paperManager.GetComponent<PaperManager>().theses;

        foreach(Thesis thesisData in thesisDatas)
        {
            authorList.Add(thesisData.author);
        }
    }

}
public class AcademyList : ListPaper
{
    List<string> academyList = new();

    public AcademyList(List<string> data)
    {
        this.paperManager = GameObject.Find("PaperManager").GetComponent<PaperManager>();
        this.academyList = data;
        //LoadData(data);
        this.ListType = LISTTYPE.ACADEMY;
    }

    public override string MakePaper()
    {
        string academies = "";

        foreach (string academy in academyList)
        {
            academies += academy + "\n";
        }

        return academies;
    }

    public override void LoadData(string data)
    {
        List<Paper> thesisDatas = paperManager.GetComponent<PaperManager>().theses;

        foreach (Thesis thesisData in thesisDatas)
        {
            academyList.Add(thesisData.academy);
        }
    }

}
public class InstitutonList : ListPaper
{
    List<string> institutonList = new();

    public InstitutonList(List<string> data)
    {
        this.paperManager = GameObject.Find("PaperManager").GetComponent<PaperManager>();
        this.institutonList = data;
        //LoadData(data);
        this.ListType = LISTTYPE.INSTITUTION;
    }

    public override string MakePaper()
    {
        string institutons = "";

        foreach (string instituton in institutonList)
        {
            institutons += instituton + "\n";
        }

        return institutons;
    }

    public override void LoadData(string data)
    {
        List<Paper> documentDatas = paperManager.GetComponent<PaperManager>().documents;

        foreach (Document documentData in documentDatas)
        {
            institutonList.Add(documentData.institution);
        }
    }

}

public class StampList
{
    public LISTTYPE listType = LISTTYPE.STAMP;

    public PaperManager paperManager;

    public List<string> stampSpriteList = new();

    public StampList(List<string> data)
    {
        this.paperManager = GameObject.Find("PaperManager").GetComponent<PaperManager>();
    }

}