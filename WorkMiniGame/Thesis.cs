using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thesis : Paper
{

    public string author { get; set; }

    public string academy { get; set; }

    public Thesis()
    {
    }

    public Thesis(string title, string academy, string author, bool isValid, TYPE type)
    {
        this.title = title;
        this.academy = academy;
        this.author = author;
        this.isValidPaper = isValid;
        this.paperType = type;
    }

}
