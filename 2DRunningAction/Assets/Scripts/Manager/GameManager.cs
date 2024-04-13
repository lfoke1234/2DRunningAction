using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isPlayTutorial;

    public bool unlockScene1;
    public bool unlockScene2;

    private Player player;
    private Transform endPos;

    public int energy;

    [Header("Text")]
    public float distance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneLoaded += OnSceneLoadAudio;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 120;

        player = PlayerManager.instance.player;
        Load();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject endPosObject = GameObject.Find("EndPos");
        if (endPosObject != null)
        {
            endPos = endPosObject.transform;
        }


    }

    private void OnSceneLoadAudio(Scene scene, LoadSceneMode mode)
    {
        switch (scene.buildIndex)
        {
            case 0:
                AudioManager.instance.PlayBGM(1);
                break;
            case 1:
                AudioManager.instance.PlayBGM(0);
                break;
            case 2:
                AudioManager.instance.PlayBGM(0);
                break;
        }
    }

    private void Update()
    {
        if (endPos != null)
        {
            if (distance >= 0)
                distance = endPos.transform.position.x - player.transform.position.x;
        }
    }

    public void SetTimeScale1() => Time.timeScale = 1.0f;

    #region Scene

    public void StartGame()
    {
        if (isPlayTutorial == false)
        {
            LoadTutorial();
        }
        else
        {
            LoadScene02();
        }
    }

    public void LoadTutorial()
    {
        player.SpeedReset();
        energy = 0;
        player.transform.position = new Vector3(0f, 1.6f, 0f);
        player.playerMaxHealth = 99999f;
        player.playerHealth = 99999f;
        SceneManager.LoadScene(1);
    }

    public void EndTutorial()
    {
        isPlayTutorial = true;
        energy = 0;
        Save();
        SceneManager.LoadScene(0);
    }

    public void LoadScene02()
    {
        player.SpeedReset();
        energy = 0;
        player.playerMaxHealth = 20f;
        player.playerHealth = 20f;
        player.transform.position = new Vector3(0f, 1.6f, 0f);
        SceneManager.LoadScene(2);
    }
    #endregion

    #region save and load
    public void Save()
    {
        PlayerPrefs.SetInt("IsPlayTutorial", isPlayTutorial ? 1 : 0);
        PlayerPrefs.SetInt("UnlockScene1", unlockScene1 ? 1 : 0);
        PlayerPrefs.SetInt("UnlockScene2", unlockScene2 ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void Load()
    {
        isPlayTutorial = PlayerPrefs.GetInt("IsPlayTutorial", 0) == 1;
        unlockScene1 = PlayerPrefs.GetInt("UnlockScene1", 0) == 1;
        unlockScene2 = PlayerPrefs.GetInt("UnlockScene2", 0) == 1;
    }

    public void DeleteSpecificSaveData()
    {
        PlayerPrefs.DeleteKey("IsPlayTutorial");
        isPlayTutorial = false;
        PlayerPrefs.DeleteKey("UnlockScene1");
        unlockScene1 = false; 
        PlayerPrefs.DeleteKey("UnlockScene2");
        unlockScene2 = false;
        PlayerPrefs.Save();
    }
    #endregion
}
