using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingGateController : MonoBehaviour
{
    public float rotSpeed = 25.0f;

    public bool gateClose = false;    

    public GameObject leftGate;
    public GameObject rightGate;

    public GameObject exitText;

    public GameObject flashLight;

    public AudioSource gateAudioSource;
    public AudioClip gateAudioClip;
    public bool isGateAudioPlaying = false;

    public bool exitGame;

    void Start()
    {
        gateAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            gateClose = true;
        }

        RotateGate();

        if(exitGame)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }

    void RotateGate()
    {
        if (gateClose)
        {            
            Cursor.lockState = CursorLockMode.Locked;

            flashLight.SetActive(false);

            exitText.SetActive(false);

            if (!isGateAudioPlaying)
            {
                isGateAudioPlaying = true;
                gateAudioSource.PlayOneShot(gateAudioClip);
            }

            leftGate.transform.Rotate(Vector3.up * rotSpeed * Time.deltaTime);
            rightGate.transform.Rotate(-Vector3.up * rotSpeed * Time.deltaTime);

            if (leftGate.transform.rotation.y >= 0 && rightGate.transform.rotation.y <= 0)
            {
                rotSpeed = 0f;

                gateClose = false;

                exitGame = true;
            }
        }
    }
}
