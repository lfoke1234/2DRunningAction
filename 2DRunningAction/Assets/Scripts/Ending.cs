using UnityEngine;

public class Ending : MonoBehaviour
{
    [SerializeField] private GameObject Scene1;
    [SerializeField] private GameObject Scene2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            Time.timeScale = 0f;

            if (GameManager.instance.energy < 100)
            {
                GameManager.instance.unlockScene1 = true;
                Scene1.SetActive(true);
            }
            else if (GameManager.instance.energy >= 100)
            {
                GameManager.instance.unlockScene2 = true;
                Scene2.SetActive(true);
            }
        }

        GameManager.instance.Save();
    }
}
