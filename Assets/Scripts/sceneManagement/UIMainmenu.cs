using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UIMainmenu : MonoBehaviour
{
    public Button Play;
    public Button Tutorial;


    private void Start()
    {
        Play.onClick.AddListener(StartGame);
        Tutorial.onClick.AddListener(StartTutorial);
    }

    private void StartGame()
    {
        scenesManager.Instance.LoadNewGame();
    }


    private void StartTutorial()
    {
        scenesManager.Instance.LoadScene(scenesManager.Scene.Tutorial);
    }
}
