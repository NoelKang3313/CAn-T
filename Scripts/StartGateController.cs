using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGateController : MonoBehaviour
{
    public float rotSpeed = 25.0f;

    public GameObject leftGate;
    public GameObject rightGate;

    public bool gateOpen = false;
    public bool gateFullyOpened = false;

    public GameObject enterText;

    public GameObject flashLight;

    public Button startButton;

    public AudioSource gateAudioSource;
    public AudioClip gateAudioClip;
    public bool isGateAudioPlaying = false;    

    void Start()
    {
        gateAudioSource = GetComponent<AudioSource>();        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            gateOpen = true;
        }

        RotateGate();
    }

    void RotateGate()
    {
        if(gateOpen)
        {
            startButton.GetComponent<Button>().interactable = true;
            Cursor.lockState = CursorLockMode.Locked;

            enterText.SetActive(false);

            if (!isGateAudioPlaying)
            {
                isGateAudioPlaying = true;
                gateAudioSource.PlayOneShot(gateAudioClip);
            }

            leftGate.transform.Rotate(-Vector3.up * rotSpeed * Time.deltaTime);
            rightGate.transform.Rotate(Vector3.up * rotSpeed * Time.deltaTime);

            if (leftGate.transform.rotation.y <= -0.9f && rightGate.transform.rotation.y >= 0.9f)
            {
                rotSpeed = 0f;

                flashLight.SetActive(true);
                Cursor.lockState = CursorLockMode.None;

                gateOpen = false;
                gateFullyOpened = true;
            }
        }
    }
}
