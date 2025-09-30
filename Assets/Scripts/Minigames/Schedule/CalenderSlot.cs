using UnityEngine;
using UnityEngine.UI;

public class CalenderSlot : MonoBehaviour
{
    public Vector2 dayPosition;
    public Color backgroundColor;

    public bool isOccupied = false;

    public Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void SetColor(Color color)
    {
        backgroundColor = color;

        if (image != null) image.color = color;
    }

    public void SetPosition(Vector2 position)
    {
        dayPosition = position; 
    }

    public Vector2 GetPosition() => dayPosition;
}
