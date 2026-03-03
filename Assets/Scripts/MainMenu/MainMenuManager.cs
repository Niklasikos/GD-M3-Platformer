using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void StartButton()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void SettingsButton()
    {
        SceneManager.LoadScene("Settings");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void GoBack()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ResetProgress()
    {
        
    }
}
