using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class setting : MonoBehaviour
{
    bool isFullScreen = true;
    public AudioMixer am;
    public AudioMixer etcMusic;
    // Start is called before the first frame update
    void Start()
    {
        am.SetFloat("MyExposedParam", PlayerPrefs.GetFloat("AudioVolume", 0));
        etcMusic.SetFloat("etcMusicParam", PlayerPrefs.GetFloat("etcVolume", 0));
    }

    public void AudioVolume(float sliderValue)
    {
        am.SetFloat("MyExposedParam", sliderValue);
        PlayerPrefs.SetFloat("AudioVolume",sliderValue);
        PlayerPrefs.Save();
    }
    public void etcVolume(float sliderValue)
    {
        am.SetFloat("etcMusicParam", sliderValue);
        PlayerPrefs.SetFloat("etcVolume", sliderValue);
        PlayerPrefs.Save();
    }
    private void Update()
    {
        if (GameObject.Find("Panel"))
        {
            GameObject.Find("SliderAudio").GetComponent<Slider>().value = PlayerPrefs.GetFloat("AudioVolume", 0);
            GameObject.Find("SliderEtc").GetComponent<Slider>().value = PlayerPrefs.GetFloat("etcVolume", 0);
        }
    }
    public void FullScreenToggle()
    {
        isFullScreen = !isFullScreen;
        Screen.fullScreen = isFullScreen;
    }
}
