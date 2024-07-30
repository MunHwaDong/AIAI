using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    private static readonly int C_THESIS_PAY = 3;

    private static readonly int C_DOCUMENT_PAY = 2;

    [SerializeField]
    private TYPE zoneType;

    [SerializeField]
    private WorkUIManager uiManager;

    public void OnPointerEnter(PointerEventData eventData)
    {
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }

    public void OnDrop(PointerEventData eventData)
    {
        Draggable paper = eventData.pointerDrag.GetComponent<Draggable>();
        TYPE iconType = eventData.pointerDrag.GetComponent<Icon>().iconType;

        if (paper != null)
        {
            if (gameObject.name == "ThesisZone")
            {
                Destroy(eventData.pointerDrag);
                uiManager.DestroyMonitorPaper();

                if (iconType == zoneType)
                {
                    uiManager.UpdateMoney(C_THESIS_PAY);
                }

                uiManager.SetNextPaper();
            }
            else if (gameObject.name == "DocumentZone")
            {
                Destroy(eventData.pointerDrag);
                uiManager.DestroyMonitorPaper();

                if (iconType == zoneType)
                {
                    uiManager.UpdateMoney(C_DOCUMENT_PAY);
                }

                uiManager.SetNextPaper();
            }
            else if (gameObject.name == "TrashZone")
            {
                Destroy(eventData.pointerDrag);
                uiManager.DestroyMonitorPaper();
                uiManager.SetNextPaper();
            }
            else
            {
                Destroy(eventData.pointerDrag);
                uiManager.DestroyMonitorPaper();
                uiManager.SetNextPaper();
            }
        }
    }
}
