using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minigameSpawner : MonoBehaviour
{

    [Header("Spawn Settings")]
    public List<GameObject> minigames;
    public List<GameObject> activeButtons = new List<GameObject>();

    public RectTransform panel;               

    public float spawnInterval = 2f;          
    public float destroyTime = 5f;

    private Coroutine spawnRoutine;

     public bool minigameActive = false;


    public void StartSpawning()
    {
        if (spawnRoutine == null)
        {
            spawnRoutine = StartCoroutine(SpawnLoop());
        }
    }

    public void StopSpawning()
    {
        if (spawnRoutine != null)
        {
            StopCoroutine(spawnRoutine);
            spawnRoutine = null;
        }
    }


    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnRandomUIObject();
        }
    }

    private void SpawnRandomUIObject()
    {
        //null check:
        if (minigames.Count == 0 || panel == null)
        {
            Debug.LogWarning("EMPTY!!");
            return;
        }

        //Select a random minigame:
        GameObject prefab = minigames[Random.Range(0, minigames.Count)];
        GameObject spawnedUI = Instantiate(prefab, panel);
        activeButtons.Add(spawnedUI);

        //Toggle on and off buttons when minigame is playing:
        spawnedUI.SetActive(!minigameActive);

        //Refrence to panel to allow random positioning:
        RectTransform spawnedRect = spawnedUI.GetComponent<RectTransform>();
        RectTransform panelRect = panel;


        //Size According to Panel(Computer Screen):
        float width = panelRect.rect.width;
        float height = panelRect.rect.height;

        //Randomized Spawn Location:
        float randomX = Random.Range(-width / 2f, width / 2f);
        float randomY = Random.Range(-height / 2f, height / 2f);
        spawnedRect.anchoredPosition = new Vector2(randomX, randomY);

        Destroy(spawnedUI, destroyTime);
    }
}
