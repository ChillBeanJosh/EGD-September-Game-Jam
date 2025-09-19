using UnityEngine;

public class Clock_manager : MonoBehaviour
{
    [Header("Rotated Object: ")]
    public GameObject rotatedObject;
    private Transform objectTransform;

    public float roundTime = 0f;

    private float tickAngle;

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


    public void RotateTick()
    {
        dayStart = true;
        objectTransform.Rotate(Vector3.forward, tickAngle);
    }


    private void Update()
    {
        timeLeft = roundTime;

        timeLeft -= ()
        if ()
        {
            RotateTick()

        }
    }
}
