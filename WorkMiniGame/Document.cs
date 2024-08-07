using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Document : Paper
{
    public string institution { get; set; }

    public string stampFilePath { get; set; }

    private GameObject stampObj;

    public Document()
    {
    }

    public Document(string title, string institution, string stampFilePath, bool isValid, TYPE type)
    {
        this.title = title;
        this.institution = institution;
        this.stampFilePath = stampFilePath;
        this.isValidPaper = isValid;
        this.paperType = type;
    }

}
