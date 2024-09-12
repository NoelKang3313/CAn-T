using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room1SoundManager : MonoBehaviour
{
    public AudioSource getKeyAudioSource;
    public AudioClip getKeyAudioClip;
    public bool getKeyAudioPlaying;

    public AudioSource getRoomKeyAudioSource;
    public AudioClip getRoomKeyAudioClip;
    public bool getRoomKeyAudioPlaying;

    void Start()
    {
        
    }
    
    void Update()
    {
        GetDrawerKeyAudio();
        GetRoomKeyAudio();
    }

    void GetDrawerKeyAudio()
    {
        if(GameManager.instance.getCabinetKey && !getKeyAudioPlaying)
        {
            getKeyAudioPlaying = true;
            getKeyAudioSource.PlayOneShot(getKeyAudioClip);
        }
    }

    void GetRoomKeyAudio()
    {
        if(GameManager.instance.getRoomKey && !getRoomKeyAudioPlaying)
        {
            getRoomKeyAudioPlaying = true;
            getRoomKeyAudioSource.PlayOneShot(getRoomKeyAudioClip);
        }
    }
}
