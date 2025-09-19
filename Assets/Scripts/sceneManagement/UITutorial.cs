using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UITutorial : MonoBehaviour
{
    public Button MainMenu;

    private void Start()
    {
        MainMenu.onClick.AddListener(StartMainMenu);
    }

    private void StartMainMenu()
    {
        scenesManager.Instance.LoadMainMenu();
    }

}
