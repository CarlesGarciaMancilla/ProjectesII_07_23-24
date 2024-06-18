using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public class GetMusic : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    // Start is called before the first frame update
    void Start()
    {
        myMixer.SetFloat("Music", Mathf.Log10(PlayerPrefs.GetFloat("MusicVolume")) * 20);
        myMixer.SetFloat("Sounds", Mathf.Log10(PlayerPrefs.GetFloat("SoundVolume")) * 20);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
