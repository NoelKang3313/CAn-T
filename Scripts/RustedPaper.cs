using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RustedPaper : MonoBehaviour
{
    public Image FKeyImage;
    public Image image;

    public GameObject paperImage;
    public Image paperImageUI;

    public GameObject itemButton;    

    AudioSource audioSource;
    public AudioClip paperAudioClip;
    public bool paperAudioPlaying = false;

    UIEntrance uIEntrance;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        uIEntrance = GameObject.Find("UIEntrance").GetComponent<UIEntrance>();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && GameManager.instance.activateUI)
        {
            FKeyImage.gameObject.SetActive(true);

            if(Input.GetKey(KeyCode.F) && !GameManager.instance.obtainPaper)
            {
                GameManager.instance.obtainPaper = true;

                itemButton.SetActive(false);

                FKeyImage.gameObject.SetActive(false);                
                paperImageUI.gameObject.SetActive(true);
                Cursor.lockState = CursorLockMode.None;

                Color imageColor = image.color;
                imageColor.a = 1.0f;
                image.color = imageColor;

                uIEntrance.buttonActive[1] = true;
            }
        }
        else if(other.gameObject.CompareTag("Player") && !GameManager.instance.activateUI)
        {
            FKeyImage.gameObject.SetActive(false);
        }

        if(GameManager.instance.obtainPaper && !paperAudioPlaying)
        {
            paperAudioPlaying = true;
            audioSource.PlayOneShot(paperAudioClip);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            FKeyImage.gameObject.SetActive(false);
        }
    }

    public void MouseVisible()
    {
        GameManager.instance.obtainPaper = false;
        GameManager.instance.activeUI = false;

        paperImage.SetActive(false);

        itemButton.SetActive(true);

        Cursor.lockState = CursorLockMode.Locked;
    }
}
