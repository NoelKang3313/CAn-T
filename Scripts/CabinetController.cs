using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CabinetController : MonoBehaviour
{
    public Image FKeyImage;

    public GameObject leftDoor;
    public GameObject rightDoor;

    BoxCollider boxCollider;
    public BoxCollider paperBoxCollider;

    public float rotSpeed = 25.0f;

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
        boxCollider = GetComponent<BoxCollider>();
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
        if (GameManager.instance.isCabinetDoorOpen)
        {
            if (!doorOpenAudioPlaying)
            {
                doorOpenAudioPlaying = true;
                audioSource.PlayOneShot(doorOpenAudioClip);
            }

            leftDoor.transform.Rotate(Vector3.up * rotSpeed * Time.deltaTime);
            rightDoor.transform.Rotate(-Vector3.up * rotSpeed * Time.deltaTime);

            FKeyImage.gameObject.SetActive(false);

            if (leftDoor.transform.rotation.y <= 0.7f && rightDoor.transform.rotation.y >= -0.7f)
            {
                rotSpeed = 0;
               
                GameManager.instance.isCabinetDoorOpen = false;
                
                paperBoxCollider.enabled = true;
                boxCollider.enabled = false;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (GameManager.instance.activateUI)
            {
                FKeyImage.gameObject.SetActive(true);

                if (Input.GetKey(KeyCode.F) && !GameManager.instance.getCabinetKey)
                {
                    StartCoroutine(DoorLockedAudioCoroutine());

                    if (!startTime)
                    {
                        startTime = true;
                        messageText.text = "Can't open it. Need a key to open";
                    }
                }

                else if (Input.GetKey(KeyCode.F) && GameManager.instance.getCabinetKey)
                {
                    GameManager.instance.isCabinetDoorOpen = true;
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

        yield return null;
    }
}
