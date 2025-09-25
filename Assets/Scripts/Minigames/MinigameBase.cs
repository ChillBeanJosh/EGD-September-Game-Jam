using UnityEngine;

public abstract class MinigameBase : MonoBehaviour
{
    protected minigameSpawner spawner;

    protected virtual void Awake()
    {
        spawner = Object.FindFirstObjectByType<minigameSpawner>();

        if (spawner == null)
            Debug.LogWarning($"{name}: spawner not found in Awake!");
    }

    public virtual void StartMinigame()
    {
        Debug.Log($"{name}: StartMinigame called");

        //Hide all currently spawned buttons
        if (spawner != null)
        {
            spawner.minigameActive = true;
            Debug.Log($"{name}: minigameActive set to true");

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
        Debug.Log($"{name}: EndMinigame called with success={success}");

        //Hide this minigame
        gameObject.SetActive(false);

        //Re-enable all buttons
        if (spawner != null)
        {
            spawner.minigameActive = false;
            Debug.Log($"{name}: minigameActive set to false");


            foreach (var button in spawner.activeButtons)
            {
                if (button != null)
                {
                    button.gameObject.SetActive(true);
                    Debug.Log($"{name}: Re-enabled button {button.name}");
                }

            }
        }
        else
        {
            Debug.LogWarning($"{name}: spawner is null in EndMinigame!");
        }
    }
}
