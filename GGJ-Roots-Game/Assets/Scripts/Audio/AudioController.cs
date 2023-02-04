using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public AudioSource audioSource;

    //Music References
    [SerializeField] private AudioClip music1_L1;
    [SerializeField] private AudioClip music1_LM;

    [SerializeField] private Slider volumeSlider;

    public static AudioController instance;

    public bool menu;
    public bool level1;

    void Start()
    {
        instance = this;
        LoadValues();

        if(menu) {
            MenuMusic();
        }

        if(level1) {
            Level1Music();
        }
    }

    // Update is called once per frame
    void Update()
    {
        VolumeSlider();
    }

    public void Level1Music() {
        audioSource.clip = music1_L1;
        audioSource.Play();
    }

    public void MenuMusic() {
        audioSource.clip = music1_LM;
        audioSource.Play();
    }


    public void VolumeSlider()
    {
        float volumeValue = volumeSlider.value;
        PlayerPrefs.SetFloat("VolumeValue", volumeValue);
        LoadValues();
    }

    void LoadValues()
    {
        float volumeValue = PlayerPrefs.GetFloat("VolumeValue");
        volumeSlider.value = volumeValue;
        AudioListener.volume = volumeValue;
    }
}
