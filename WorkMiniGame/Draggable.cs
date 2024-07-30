using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;

    private RectTransform canvasTransform;

    private float minX;
    private float minY;
    private float maxX;
    private float maxY;

    private Vector2 offset;

    public Canvas canvas { get; set; }

    void Awake()
    {
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        GetComponent<CanvasGroup>().blocksRaycasts = false;

        Vector2 localPos;
        rectTransform = this.GetComponent<RectTransform>();
        canvasTransform = canvas.GetComponent<RectTransform>();
        this.transform.SetAsLastSibling();

        minX = -(canvasTransform.sizeDelta.x - (rectTransform.sizeDelta.x * rectTransform.localScale.x)) * 0.5f;
        minY = -(canvasTransform.sizeDelta.y - (rectTransform.sizeDelta.y * rectTransform.localScale.y)) * 0.5f;
        maxX = (canvasTransform.sizeDelta.x - (rectTransform.sizeDelta.x * rectTransform.localScale.x)) * 0.5f;
        maxY = (canvasTransform.sizeDelta.y - (rectTransform.sizeDelta.y * rectTransform.localScale.y)) * 0.5f;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out localPos))
        {
            offset = rectTransform.anchoredPosition - localPos;
        }

    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPos;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasTransform, eventData.position, eventData.pressEventCamera, out localPos))
        {
            Vector2 newPosition = localPos + offset;
            //anchor∞° 0.5, 0,5¿Ã¥œ.
            newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
            newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

            rectTransform.anchoredPosition = newPosition;
        }

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

}
