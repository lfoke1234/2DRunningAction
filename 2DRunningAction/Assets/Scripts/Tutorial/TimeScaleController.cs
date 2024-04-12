using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeScaleController : MonoBehaviour
{
    public void SetToggle(bool value)
    {
        PlayerManager.instance.player.ToggleTest(value);
    }


    public void SetTimeScale()
    {
        Time.timeScale = 1.0f;
    }
    
    public void SetTimeScale2()
    {
        Time.timeScale = 0.0f;
    }

    public void EndTutorial()
    {
        GameManager.instance.EndTutorial();
    }

    public void StartGame()
    {
        GameManager.instance.StartGame();
    }

    public void LoadTutorial()
    {
        GameManager.instance.LoadTutorial();
    }

    public void GoTitle()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
