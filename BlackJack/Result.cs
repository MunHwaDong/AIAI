using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Result : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI studyPointText;

    public void PrintResult(int studyPoint)
    {
        studyPointText.text = "";
        studyPointText.text = "Study Point : " + studyPoint.ToString();
    }
}
