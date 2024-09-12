using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorageDoorController : MonoBehaviour
{
    public float rotSpeed = 25.0f;

    public Image FKeyImage;

    public Text messageText;
    private float textTime;
    private float endTextTime = 3.0f;
    private bool startTime = false;

    AudioSource audioSource;
    public AudioClip doorLockedAudioClip;
    public bool doorLockedAudioPlaying = false;    

    public AudioClip doorOpenAudioClip;
    public bool doorOpenAudioPlaying = false;

    public AudioClip scratchingAudioClip;
    public bool scratchingAudioPlaying = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();        
    }

    void Update()
    {
        RotateDoor();

        TextTimer();
    }

    void TextTimer()
    {
        if (startTime)
        {
            textTime += Time.deltaTime;

            if (textTime >= endTextTime)
            {
                startTime = false;
                textTime = 0;
                messageText.text = "";
            }
        }
    }

    void RotateDoor()
    {
        if(GameManager.instance.isDoorOpen)
        {            
            if(!doorOpenAudioPlaying)
            {
                doorOpenAudioPlaying = true;
                audioSource.PlayOneShot(doorOpenAudioClip);
            }

            gameObject.transform.Rotate(-Vector3.up * rotSpeed * Time.deltaTime);

            FKeyImage.gameObject.SetActive(false);

            if(gameObject.transform.rotation.y <= 0.7f)
            {
                GameManager.instance.isDoorOpen = false;
                rotSpeed = 0f;

                StartCoroutine(ScratchingAudioCoroutine());
            }
        }
    }    

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && GameManager.instance.activateUI)
        {
            FKeyImage.gameObject.SetActive(true);

            if (Input.GetKey(KeyCode.F) && GameManager.instance.getStorageKey)
            {
                GameManager.instance.isDoorOpen = true;
            }
            else if(Input.GetKey(KeyCode.F) && !GameManager.instance.getStorageKey)
            {
                StartCoroutine(DoorLockedAudioCoroutine());                

                if (!startTime)
                {
                    startTime = true;
                    messageText.text = "Can't open it. Need a key to open";
                }
            }
        }
        else if (other.gameObject.CompareTag("Player") && !GameManager.instance.activateUI)
        {
            FKeyImage.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            FKeyImage.gameObject.SetActive(false);
        }
    }

    IEnumerator DoorLockedAudioCoroutine()
    {
        if (!doorLockedAudioPlaying)
        {
            doorLockedAudioPlaying = true;
            audioSource.PlayOneShot(doorLockedAudioClip);
        }

        yield return new WaitForSeconds(2.0f);

        doorLockedAudioPlaying = false;        
    }

    IEnumerator ScratchingAudioCoroutine()
    {
        yield return new WaitForSeconds(3.0f);

        if(!scratchingAudioPlaying)
        {
            scratchingAudioPlaying = true;
            audioSource.PlayOneShot(scratchingAudioClip);
        }
    }
}
