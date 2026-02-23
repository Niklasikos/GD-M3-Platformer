using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    public Scene currentScene;
    public string currentSceneName;

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        currentSceneName = currentScene.name;
    }

    public void LoadCurrentScene()
    {
        SceneManager.LoadScene(currentSceneName);
        Time.timeScale = 1;
    }

    public void LoadLevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
        Time.timeScale = 1;
    }
}
