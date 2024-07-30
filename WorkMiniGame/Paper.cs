using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum TYPE { THESIS, DOCUMENT, TRASH }

public class Paper : MonoBehaviour
{
    public string title { get; set; }

    public bool isValidPaper { get; set; }

    public TYPE paperType { get; set; }

    public SpriteRenderer paperSprite { get; set; }

    public bool toggleIconToPaper { get; set; } = true;

    public GameObject iconObj { get; set; }

    public GameObject paperObj { get; set; }

    public Paper()
    {
        isValidPaper = false;
    }

    public Paper(bool isVaild)
    {
        isValidPaper = isVaild;
    }

    public void MoveTransform(Transform pos, float dotweenTime = 0f)
    {
        transform.DOMove(pos.position, dotweenTime);
        transform.DORotateQuaternion(pos.rotation, dotweenTime);
    }

    public void TransformPaperIcon()
    {
        if(toggleIconToPaper)
        {
            paperObj.SetActive(true);
            iconObj.SetActive(false);
        }
        else
        {
            paperObj.SetActive(false);
            iconObj.SetActive(true);
        }
    }

}
