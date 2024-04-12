using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadUI : MonoBehaviour
{
    [SerializeField] private GameObject dead;

    public void SetDeadUI()
    {
        dead.SetActive(true);
    }
}
