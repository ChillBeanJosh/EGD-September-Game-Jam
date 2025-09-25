using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class badMinigames : MonoBehaviour
{
    public List<MinigameBase> possibleGames;
    private MinigameBase chosenGame;

    public static List<MinigameBase> activeMinigames = new List<MinigameBase>();


    [Header("Work Day Increment Range")]
    public int minimumDay = 1;
    public int maximumDay = 3;

    private int workDaysIncremented;

    private void Start()
    {
        if (possibleGames.Count > 0)
        {
            //Assign a Random Minigame:
            chosenGame = possibleGames[Random.Range(0, possibleGames.Count)];
            //chosenGame.gameObject.SetActive(false);
        }
        workDaysIncremented = Random.Range(minimumDay, maximumDay + 1);

        Button button = GetComponent<Button>();
        if (button != null) button.onClick.AddListener(OnBadMinigameSelected);
    }

    private void OnBadMinigameSelected()
    {
        if (chosenGame != null)
        {
            chosenGame.StartMinigame();
            activeMinigames.Add(chosenGame);
        }

        dayController.Instance.AddDays(workDaysIncremented);
        Destroy(gameObject);
    }
}
