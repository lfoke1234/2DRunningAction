using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleBGM : MonoBehaviour
{
    private AudioManager audioManager;

    

    void Start()
    {
        audioManager = AudioManager.instance.GetComponent<AudioManager>();

        this.GetComponent<Toggle>().onValueChanged.AddListener(audioManager.ToggleBGM);
    }
    private void Update()
    {
        if(audioManager.bgmActive == false)
        {
            GetComponent<Toggle>().isOn = false;
        }
        else
        {
            GetComponent<Toggle>().isOn = true;
        }
    }
}
