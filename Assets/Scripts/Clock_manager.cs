using UnityEngine;

public class Clock_manager : MonoBehaviour
{
    [Header("Rotated Object: ")]
    public GameObject rotatedObject;
    private Transform objectTransform;

    public float roundTime = 0f;

    public float tickAngle;

    private float timeLeft;

    public bool dayStart = false;


    private void Start()
    {
        //Object Check:
        if (rotatedObject != null)
        {
            objectTransform = rotatedObject.transform;
        }

        //Angle in relation to tick on time:
        tickAngle = 360f / roundTime;
    }
}
