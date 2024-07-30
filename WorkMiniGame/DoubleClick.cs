using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DoubleClick : MonoBehaviour, IPointerClickHandler
{
    public Paper paper { get; set; }

    [SerializeField]
    private WorkUIManager uiManager;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 2)
        {
            Debug.Log("Double Click");
            paper.toggleIconToPaper = !paper.toggleIconToPaper;
            paper.TransformPaperIcon();
        }
    }

}
