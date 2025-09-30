using UnityEngine;

public abstract class MinigameBase : MonoBehaviour
{
    protected minigameSpawner spawner;

    protected virtual void Awake()
    {
        spawner = Object.FindFirstObjectByType<minigameSpawner>();
    }

    public virtual void StartMinigame()
    {

        //Hide all currently spawned buttons
        if (spawner != null)
        {
            spawner.minigameActive = true;

            foreach (var button in spawner.activeButtons)
            {
                if (button != null && button.gameObject != this.gameObject)
                    button.gameObject.SetActive(false);
            }
        }

        //Show this minigame UI
        gameObject.SetActive(true);
    }

    public virtual void EndMinigame(bool success)
    {
        //Hide this minigame
        gameObject.SetActive(false);

        //Re-enable all buttons
        if (spawner != null)
        {
            spawner.minigameActive = false;

            foreach (var button in spawner.activeButtons)
            {
                if (button != null)
                {
                    button.gameObject.SetActive(true);
                }

            }
        }
    }
}
