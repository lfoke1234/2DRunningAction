using UnityEngine;
using UnityEngine.UI;

public class ToggleSFX : MonoBehaviour
{
    private AudioManager audioManager;

    void Start()
    {
        audioManager = AudioManager.instance.GetComponent<AudioManager>();

        this.GetComponent<Toggle>().onValueChanged.AddListener(audioManager.ToggleSFX);
    }

    private void Update()
    {
        if (audioManager.sfxActive == false)
        {
            GetComponent<Toggle>().isOn = false;
        }
        else
        {
            GetComponent<Toggle>().isOn = true;
        }
    }
}
