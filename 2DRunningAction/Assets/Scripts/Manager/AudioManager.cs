using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private string bgmParameter;
    [SerializeField] private string sfxParameter;

    public bool bgmActive;
    public bool sfxActive;

    [SerializeField] private AudioSource[] sfx;
    [SerializeField] private AudioSource[] bgm;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
    }

    public void PlaySFX(int _sfxIndex)
    {
        if (sfx[_sfxIndex].isPlaying)
        {
            StopSFX(_sfxIndex);
        }

        if (_sfxIndex < sfx.Length)
        {
            sfx[_sfxIndex].Play();
        }
    }

    public void StopSFX(int _sfxIndex) => sfx[_sfxIndex].Stop();

    public void PlayBGM(int index)
    {
        for (int j = 0; j < bgm.Length; j++)
        {
            bgm[j].Stop();
        }

        if (index >= 0 && index < bgm.Length)
        {
            bgm[index].Play();
        }
        else
        {
            Debug.LogError("BGM index out of range: " + index);
        }
    }


    public void StopBGM()
    {
        for (int i = 0; i < bgm.Length; i++)
        {
            bgm[i].Stop();
        }
    }

    public void ToggleBGM(bool value)
    {
        bgmActive = value;

        if (bgmActive)
            audioMixer.SetFloat(bgmParameter, 0f);
        else
            audioMixer.SetFloat(bgmParameter, -80f);
    }

    public void ToggleSFX(bool value)
    {
        sfxActive = value;

        if (sfxActive)
            audioMixer.SetFloat(sfxParameter, 0f);
        else
            audioMixer.SetFloat(sfxParameter, -80f);
    }

}
