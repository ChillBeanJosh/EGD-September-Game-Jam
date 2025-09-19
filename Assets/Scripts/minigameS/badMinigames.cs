using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class badMinigames : MonoBehaviour
{
    public List<Image> possibleGames;

    [Header("Work Day Increment Range")]
    public int minimumDay = 1;
    public int maximumDay = 3;

    private Image choseGame;
    private int workDaysIncremented;

    private void Start()
    {
        if (possibleGames.Count > 0)
        {
            //Assign a Random Minigame:
            choseGame = possibleGames[Random.Range(0, possibleGames.Count)];
            choseGame.gameObject.SetActive(false);
        }
        workDaysIncremented = Random.Range(minimumDay, maximumDay + 1);

        Button button = GetComponent<Button>();
        if (button != null) button.onClick.AddListener(OnBadMinigameSelected);
    }

    private void OnBadMinigameSelected()
    {
        if (choseGame != null) choseGame.gameObject.SetActive(true);

        dayController.Instance.AddDays(workDaysIncremented);

        Destroy(gameObject);
    }
}
