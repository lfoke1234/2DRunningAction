using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI distance;
    [SerializeField] private TextMeshProUGUI energy;

    private void Start()
    {
        Debug.Log("load ingame");
    }
    private void Update()
    {
        Distance();
    }

    private void Distance()
    {
        distance.text = GameManager.instance.distance.ToString("#,#") + " M";
        energy.text = GameManager.instance.energy.ToString();
    }
}
