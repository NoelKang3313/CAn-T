using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceSoundManager : MonoBehaviour
{   
    public AudioSource gateAudioSource;
    public AudioClip gateAudioClip;
    public bool gateAudioPlaying = false;

    public AudioSource catAudioSource;
    public AudioClip catFightingAudioClip;
    public bool catFightingAudioPlaying = false;

    public AudioSource storageKeyAudioSource;
    public AudioClip storageKeyAudioClip;
    public bool storageKeyAudioPlaying = false;

    public AudioSource backDoorKeyAudioSource;
    public AudioClip backDoorKeyAudioClip;
    public bool backDoorKeyAudioPlaying = false;

    void Start()
    {
        
    }

    void Update()
    {
        PlayGateAudio();
        PlayCatFightingAudio();
        PlayStorageKeyAudio();
        PlayBackDoorKeyAudio();
    }

    void PlayGateAudio()
    {
        if(GameManager.instance.isEntranceGateOpen && !gateAudioPlaying)
        {
            gateAudioPlaying = true;
            gateAudioSource.PlayOneShot(gateAudioClip);
        }
    }

    void PlayCatFightingAudio()
    {
        if(GameManager.instance.isEntranceGateOpen && !catFightingAudioPlaying)
        {
            StartCoroutine(CatFightingAudioCouroutine());
        }
    }

    void PlayStorageKeyAudio()
    {
        if(GameManager.instance.getStorageKey && !storageKeyAudioPlaying)
        {
            storageKeyAudioPlaying = true;
            storageKeyAudioSource.PlayOneShot(storageKeyAudioClip);
        }
    }

    void PlayBackDoorKeyAudio()
    {
        if(GameManager.instance.getBackDoorKey && !backDoorKeyAudioPlaying)
        {
            backDoorKeyAudioPlaying = true;
            backDoorKeyAudioSource.PlayOneShot(backDoorKeyAudioClip);
        }
    }

    IEnumerator CatFightingAudioCouroutine()
    {
        catFightingAudioPlaying = true;

        yield return new WaitForSeconds(10.0f);
        
        catAudioSource.PlayOneShot(catFightingAudioClip);
    }    
}
