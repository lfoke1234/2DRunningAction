using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private bool isPlayTutorial;

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
        Debug.Log("load ingame");
        player = PlayerManager.instance.player;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject endPosObject = GameObject.Find("EndPos");
        if (endPosObject != null)
        {
            endPos = endPosObject.transform;
        }
        else
        {
            Debug.Log("Don't find EndPos");
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
    public void EndTutorial()
    {
        isPlayTutorial = true;
        energy = 0;
        LoadScene02();
    }

    public void StartGame()
    {
        if (isPlayTutorial == false)
        {
            player.transform.position = new Vector3(0f, 1.6f, 0f);
            player.playerMaxHealth = 99999f;
            player.playerHealth = 99999f;
            SceneManager.LoadScene(1);
        }
        else
        {
            player.playerMaxHealth = 20f;
            player.playerHealth = 20f;
            LoadScene02();
        }
    }

    private void LoadScene02()
    {
        player.transform.position = new Vector3(0f, 1.6f, 0f);
        endPos.transform.position = new Vector3(181f, 11f, 0f);
        SceneManager.LoadScene(2);
    }
    #endregion
}
