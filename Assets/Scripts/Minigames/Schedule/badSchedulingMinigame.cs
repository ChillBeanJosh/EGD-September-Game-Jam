using UnityEngine;
using System.Collections.Generic;

public class badSchedulingMinigame : MinigameBase
{
    public static badSchedulingMinigame Instance;

    [Header("References")]
    public SchedulingUI uiManager;

    [Header("Grid Settings")]
    public int gridWidth = 7;  //days per week
    public int gridHeight = 5; //total weeks

    public Vector2 slotSpacing = new Vector2(100, 100);
    public float slotAssignmentChance = 0.20f; //goes through the entire grid, and adds a slot if chance is rolled.

    [Header("Item Settings")]
    public Color draggableColor = Color.green;
    public Color targetColor = Color.black;
   

    private List<CalenderSlot> targetSlots = new List<CalenderSlot>();
    private List<ItemDragger> draggableItems = new List<ItemDragger>();

    protected override void Awake()
    {
        base.Awake();
        Instance = this;
    }

    public override void StartMinigame()
    {
        base.StartMinigame();

        GenerateGrid();
        GenerateShapes();

        uiManager.ShowUI();
    }

    public override void EndMinigame(bool success)
    {
        uiManager.ClearUI();
        uiManager.HideUI();

        base.EndMinigame(success);
    }

    private void GenerateGrid()
    {
        targetSlots.Clear();

        for (int y = 0; y < gridHeight; y++) //for each column
        {
            for (int x = 0; x < gridWidth; x++) //for each row
            {
                Vector2 pos = new Vector2(x * slotSpacing.x, -y * slotSpacing.y);
                CalenderSlot slot = uiManager.SpawnGridSlot(pos, Color.clear);

                //randomly assign target color to some slots:
                if (Random.value < slotAssignmentChance) 
                {
                    slot.SetColor(targetColor);
                    targetSlots.Add(slot);
                }
            }
        }

    }


    private void GenerateShapes()
    {
        draggableItems.Clear();

        HashSet<Color> usedColors = new HashSet<Color>();

        List<ItemDragger> tempDraggables = new List<ItemDragger>();

        for (int i = 0; i < targetSlots.Count; i++)
        {
            //unique random color:
            Color newColor;
            do
            {
                newColor = Random.ColorHSV
                    (
                        0f, 1f,     //Hue
                        0.2f, 1f,   //Saturation
                        0.2f, 1f    //Brightness
                    );
            }
            while (usedColors.Contains(newColor));

            usedColors.Add(newColor);

            //assign slot color:
            targetSlots[i].SetColor(newColor);

            //spawn draggable with same color:
            Vector2 pos = new Vector2(i * slotSpacing.x, -gridHeight * slotSpacing.y - 50); 
            ItemDragger shape = uiManager.SpawnDraggableShape(pos, draggableColor);

            //assign target slot:
            shape.Initialize(newColor);
            shape.targetSlot = targetSlots[i];

            //linear unrandomized list:
            tempDraggables.Add(shape);
        }

        //shuffle the list:
        for (int i = 0; i < tempDraggables.Count; i++)
        {
            ItemDragger temp = tempDraggables[i];

            int randomIndex = Random.Range(i, tempDraggables.Count);

            tempDraggables[i] = tempDraggables[randomIndex];
            tempDraggables[randomIndex] = temp;
        }

        //reposition draggables randomly:
        for (int i = 0; i < tempDraggables.Count; i++)
        {
            tempDraggables[i].GetComponent<RectTransform>().anchoredPosition =
                new Vector2(i * slotSpacing.x, -gridHeight * slotSpacing.y - 50);
        }

        //save final order:
        draggableItems = tempDraggables;
    }


    public void CheckCompletion()
    {
        foreach (var slot in targetSlots)
        {
            if (!slot.isOccupied) return; 
        }

        EndMinigame(true);
    }
}

