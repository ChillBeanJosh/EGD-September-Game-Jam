using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UIWinLose : MonoBehaviour
{
    public Button PlayAgain;
    public Button MainMenu;


    private void Start()
    {
        PlayAgain.onClick.AddListener(StartGame);
        MainMenu.onClick.AddListener(StartMainMenu);
    }

    private void StartGame()
    {
        scenesManager.Instance.LoadNewGame();
    }


    private void StartMainMenu()
    {
        scenesManager.Instance.LoadMainMenu();
    }
}
