using System.Collections.Generic;
using UnityEngine;

public class SchedulingUI : MonoBehaviour
{
    public GameObject minigamePanel;
    public RectTransform calenderParent;
    public RectTransform draggableParent;


    public GameObject calenderSlotPrefab;
    public GameObject draggableItemPrefab;

    public List<CalenderSlot> calenderSlots = new List<CalenderSlot>();
    public List<ItemDragger> draggableShapes = new List<ItemDragger>();


    public void ShowUI() => minigamePanel.SetActive(true);
    public void HideUI() => minigamePanel.SetActive(false);


    public void ClearUI()
    {
        foreach (Transform t in draggableParent) Destroy(t.gameObject);
        draggableShapes.Clear();

        foreach (Transform t in calenderParent) Destroy(t.gameObject);
        calenderSlots.Clear();
    }

    public CalenderSlot SpawnGridSlot(Vector2 position, Color color)
    {
        GameObject slotObj = Object.Instantiate(calenderSlotPrefab, calenderParent);
        slotObj.GetComponent<RectTransform>().anchoredPosition = position;

        CalenderSlot slot = slotObj.GetComponent<CalenderSlot>();
        slot.SetPosition(position);
        slot.SetColor(color);

        calenderSlots.Add(slot);
        return slot;
    }


    public ItemDragger SpawnDraggableShape(Vector2 position, Color color)
    {
        GameObject dragObj = Object.Instantiate(draggableItemPrefab, draggableParent);
        dragObj.GetComponent<RectTransform>().anchoredPosition = position;

        ItemDragger shape = dragObj.GetComponent<ItemDragger>();

        draggableShapes.Add(shape);
        return shape;
    }
}
