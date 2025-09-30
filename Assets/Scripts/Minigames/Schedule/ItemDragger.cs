using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDragger : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private RectTransform rect;
    private Canvas canvas;

    private Vector2 originalPosition;
    public Color itemColor;

    //Gets Assigned when making the list:
    public Image image;
    public CalenderSlot targetSlot;
    public float snapThreshold = 50f;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        originalPosition = rect.anchoredPosition;


        canvas = GetComponentInParent<Canvas>();

        if (image == null) image = GetComponent<Image>();
    }

    public void Initialize(Color color)
    {
        itemColor = color;
        if (image != null) image.color = color; 
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = rect.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rect.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }



    public void OnEndDrag(PointerEventData eventData)
    {
        if (targetSlot != null && !targetSlot.isOccupied)
        {
            float distance = Vector2.Distance(rect.anchoredPosition, targetSlot.GetPosition());

            if (distance <= snapThreshold)
            {
                //snap into place:
                rect.anchoredPosition = targetSlot.GetPosition();
                targetSlot.isOccupied = true;

                //disable further dragging
                GetComponent<CanvasGroup>().blocksRaycasts = false;

                badSchedulingMinigame.Instance.CheckCompletion();
                return;
            }
        }

        //return to normal if failed:
        rect.anchoredPosition = originalPosition;
    }
}
