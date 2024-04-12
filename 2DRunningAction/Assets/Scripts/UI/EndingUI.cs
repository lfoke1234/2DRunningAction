using UnityEngine;

public class EndingUI : MonoBehaviour
{
    [SerializeField] private GameObject lock1;
    [SerializeField] private GameObject unlock1;

    [SerializeField] private GameObject lock2;
    [SerializeField] private GameObject unlock2;

    private void OnEnable()
    {
        if (GameManager.instance.unlockScene1)
        {
            lock1.SetActive(false);
            unlock1.SetActive(true);
        }
        else if (GameManager.instance.unlockScene1 == false)
        {
            lock1.SetActive(true);
            unlock1.SetActive(false);
        }

        if (GameManager.instance.unlockScene2)
        {
            lock2.SetActive(false);
            unlock2.SetActive(true);
        }
        else if (GameManager.instance.unlockScene2 == false)
        {
            lock2.SetActive(true);
            unlock2.SetActive(false);
        }
    }
}
