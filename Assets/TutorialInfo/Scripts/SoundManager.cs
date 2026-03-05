using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public Slider volumeSlider;

    void Start()
    {
        if (PlayerPrefs.HasKey("sound"))
            LoadVolume();
        else
        {
            PlayerPrefs.SetFloat("sound", 1);
        }
    }

    public void SetVolume()
    {
        AudioListener.volume= volumeSlider.value;
        SaveVolume();
    }
    public void SaveVolume()
    {
        PlayerPrefs.SetFloat("sound",volumeSlider.value);
    }
    public void LoadVolume()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("sound");
    }
}