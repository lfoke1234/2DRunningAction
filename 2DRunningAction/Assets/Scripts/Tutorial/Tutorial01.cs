using UnityEngine;

public class Tutorial01 : MonoBehaviour
{
    [SerializeField] private GameObject tutorialPanel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = PlayerManager.instance.player;

        if (player != null)
        {
            tutorialPanel.SetActive(true);
            Time.timeScale = 0.0f;
        }
    }
}
