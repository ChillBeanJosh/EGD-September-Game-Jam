using UnityEngine;

public class BadTypingMinigame : MinigameBase
{
    [Header("References")]
    public GameObject minigameUI;
    public inputReader inputReader;
    public typingUI typingUI;

    public override void StartMinigame()
    {
        base.StartMinigame();

        minigameUI.SetActive(true);
        inputReader.CreatePattern();      
    }

    public override void EndMinigame(bool success)
    {
        minigameUI.SetActive(false);

        base.EndMinigame(success);
    }

    public void OnCompleted()
    {
        EndMinigame(true);
    }
}
