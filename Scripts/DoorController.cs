using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorController : MonoBehaviour
{
    public Image FKeyImage;

    public Text messageText;
    private float textTime;
    private float endTextTime = 3.0f;
    private bool startTime = false;

    AudioSource doorAudioSource;
    public AudioClip doorAudioClip;
    public bool doorAudioPlaying = false;

    void Start()
    {
        doorAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
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

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(GameManager.instance.activateUI)
            {
                FKeyImage.gameObject.SetActive(true);

                if(Input.GetKey(KeyCode.F) && !GameManager.instance.getBackDoorKey)
                {
                    if (!doorAudioPlaying)
                    {
                        StartCoroutine(DoorAudioCoroutine());
                        StopCoroutine(DoorAudioCoroutine());
                    }

                    if(!startTime)
                    {
                        startTime = true;
                        messageText.text = "Can't open it. Need a key to open";
                    }
                }
                else if(Input.GetKey(KeyCode.F) & GameManager.instance.getBackDoorKey)
                {
                    if (!doorAudioPlaying)
                    {
                        StartCoroutine(DoorAudioCoroutine());
                        StopCoroutine(DoorAudioCoroutine());
                    }

                    if (!startTime)
                    {
                        startTime = true;
                        messageText.text = "Key doesn't fit. Not the right door";
                    }
                }
            }
            else
            {
                FKeyImage.gameObject.SetActive(false);
            }
        }        
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            FKeyImage.gameObject.SetActive(false);
        }
    }

    IEnumerator DoorAudioCoroutine()
    {
        if(!doorAudioPlaying)
        {
            doorAudioPlaying = true;
            doorAudioSource.PlayOneShot(doorAudioClip);
        }

        yield return new WaitForSeconds(2.0f);

        doorAudioPlaying = false;

        yield return null;
    }
}
