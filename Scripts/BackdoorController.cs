using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackdoorController : MonoBehaviour
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

    void Start()
    {        
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        TextTimer();

        RotateDoor();
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
        if(GameManager.instance.isBackDoorOpen)
        {
            if(!doorOpenAudioPlaying)
            {
                doorOpenAudioPlaying = true;
                audioSource.PlayOneShot(doorOpenAudioClip);
            }            

            gameObject.transform.Rotate(Vector3.up * rotSpeed * Time.deltaTime);

            FKeyImage.gameObject.SetActive(false);

            if(gameObject.transform.rotation.y <= 0.7f)
            {
                rotSpeed = 0;

                GameManager.instance.getBackDoorKey = false;
                GameManager.instance.isBackDoorOpen = false;
                GameManager.instance.enterBuilding = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if (GameManager.instance.activateUI)
            {
                FKeyImage.gameObject.SetActive(true);

                if (Input.GetKey(KeyCode.F) && !GameManager.instance.getBackDoorKey)
                {
                    StartCoroutine(DoorLockedAudioCoroutine());                    

                    if (!startTime)
                    {
                        startTime = true;
                        messageText.text = "Can't open it. Need a key to open";
                    }
                }

                else if (Input.GetKey(KeyCode.F) && GameManager.instance.getBackDoorKey)
                {                   
                    GameManager.instance.isBackDoorOpen = true;
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

    IEnumerator DoorLockedAudioCoroutine()
    {
        if (!doorLockedAudioPlaying)
        {
            doorLockedAudioPlaying = true;
            audioSource.PlayOneShot(doorLockedAudioClip);
        }

        yield return new WaitForSeconds(2.0f);

        doorLockedAudioPlaying = false;

        yield return null;
    }
}
