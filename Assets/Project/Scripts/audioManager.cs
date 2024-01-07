using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class audioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;

    // Start is called before the first frame update
    public void SetMusicVolume() 
    {
        float volume = musicSlider.value;
        myMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
    }


    public void SetSoundsVolume()
    {
        float volume = soundSlider.value;
        myMixer.SetFloat("Sounds", Mathf.Log10(volume) * 20);
    }
}
