using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer masterEffects, masterMusic;
    [Space]
    public AudioSource backgroundMusic, player_Hit, playerDeath;

    public static AudioManager instance; 

    [Range(-80f, 20f)]
    public float masterVol, effectsVol;
    public Slider masterSlider, effectsSlider;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(instance.gameObject);
            instance = this; 
        }

        PlayAudio(backgroundMusic);

    }

    void Start()
    {
        masterSlider.value = masterVol;
        effectsSlider.value = effectsVol;

        masterSlider.minValue = -80f;
        masterSlider.maxValue = 20f;

        effectsSlider.minValue = -80f;
        effectsSlider.maxValue = 20f;
    }

    void Update()
    {
        MasterVolume();
        EffectsVolume(); 
    }

    public void MasterVolume()
    {
        masterMusic.SetFloat("Master Volume", masterSlider.value);
    }

    public void EffectsVolume()
    {
        masterEffects.SetFloat("Effects Volume", effectsSlider.value);
    }

    public void PlayAudio(AudioSource audio)
    {
        audio.Play();
    }
}
