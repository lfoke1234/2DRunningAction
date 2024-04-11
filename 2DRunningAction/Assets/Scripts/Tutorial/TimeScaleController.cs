using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeScaleController : MonoBehaviour
{

    public void SetTimeScale()
    {
        Time.timeScale = 1.0f;
    }

    public void Go()
    {
        SceneManager.LoadScene(2);
    }
}
