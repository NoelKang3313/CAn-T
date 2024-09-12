using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSoundManager : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip catAudioClip;
    public bool isCatAudioPlaying = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        PlayCatAudio();
    }

    void PlayCatAudio()
    {
        if(GameManager.instance.startGame && !isCatAudioPlaying)
        {
            isCatAudioPlaying = true;
            audioSource.PlayOneShot(catAudioClip);
        }
    }
}
