using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class dayController : MonoBehaviour
{
    public static dayController Instance { get; private set; }

    public Clock_manager clockManager;
    public minigameSpawner gameSpawner;

    private float rotatedAmount = 0f;
    private bool isDayRunning = false;

    [Header("Day Tracking")]
    public int currentDay = 1;
    public int dayLimit = 31;
    public TextMeshProUGUI currentDayText;


    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            eventStartDay();
        }
    }

    public void eventStartDay()
    {
        if (!isDayRunning && clockManager != null && clockManager.roundTime > 0f) StartCoroutine(RunDayCycle());
    }


    private IEnumerator RunDayCycle()
    {
        Debug.Log("STARTING THE DAY!!!");

        isDayRunning = true;
        rotatedAmount = 0f;
        clockManager.dayStart = true;

        float degreesPerSecond = 360f / clockManager.roundTime;

        if (gameSpawner != null) gameSpawner.StartSpawning();

        while (rotatedAmount < 360f)
        {
            float deltaRotation = degreesPerSecond * Time.deltaTime;
            clockManager.rotatedObject.transform.Rotate(Vector3.back, deltaRotation);

            rotatedAmount += deltaRotation;

            yield return null;
        }

        eventEndDay();
    }

    private void eventEndDay()
    {
        Debug.Log("Day Ended!");
        isDayRunning = false;

        if (gameSpawner != null) gameSpawner.StopSpawning();
        UpdateDayUI();

        if (currentDay >= dayLimit) scenesManager.Instance.LoadScene(scenesManager.Scene.GameOver);
    }

    public void AddDays(int daysToAdd)
    {
        currentDay += daysToAdd;
        Debug.Log($"Days incremented by {daysToAdd}, now on day {currentDay}");
    }

    private void UpdateDayUI()
    {
        if (currentDayText != null)
        {
            currentDayText.text = $"Day {currentDay}";
        }
    }
}
