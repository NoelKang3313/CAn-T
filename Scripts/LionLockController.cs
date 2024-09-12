using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LionLockController : MonoBehaviour
{
    public Image FKeyImage;
    public GameObject backdoorKey;    

    BoxCollider boxCollider;
    public GameObject messageText;

    public Image lockPanel;
    public GameObject itemButton;

    [SerializeField]
    int[] currentNumber = { 0, 0, 0, 0 };

    public Image[] numberSprites;
    public Button[] upButtons;
    public Button[] downButtons;
    public Sprite[] numberImages;

    AudioSource audioSource;
    public AudioClip buttonAudioClip;
    public bool buttonAudioPlaying = false;

    public AudioClip keyAudioClip;
    public bool keyAudioPlaying = false;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();

        audioSource = GetComponent<AudioSource>();

        for(int i = 0; i < upButtons.Length; i++)
        {
            int number = i;
            upButtons[i].onClick.AddListener(() => UpButtonClicked(number));
        }

        for(int i = 0; i < downButtons.Length; i++)
        {
            int number = i;
            downButtons[i].onClick.AddListener(() => DownButtonClicked(number));
        }
    }

    void Update()
    {
        SolveLock();
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && GameManager.instance.activateUI)
        {
            FKeyImage.gameObject.SetActive(true);

            if (Input.GetKey(KeyCode.F))
            {
                GameManager.instance.solvingLock = true;                

                lockPanel.gameObject.SetActive(true);
                itemButton.SetActive(false);
                messageText.SetActive(false);

                Cursor.lockState = CursorLockMode.None;
            }

            if(GameManager.instance.solvingLock)
            {
                FKeyImage.gameObject.SetActive(false);
            }
        }
        else if(other.gameObject.CompareTag("Player") && !GameManager.instance.activateUI)
        {
            FKeyImage.gameObject.SetActive(false);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            FKeyImage.gameObject.SetActive(false);
        }
    }

    void UpButtonClicked(int number)
    {
        StartCoroutine(ButtonAudioClick());

        if (currentNumber[number] >= 9)
        {
            currentNumber[number] = 0;
            numberSprites[number].GetComponent<Image>().sprite = numberImages[currentNumber[number]];
        }
        else
        {
            currentNumber[number]++;
            numberSprites[number].GetComponent<Image>().sprite = numberImages[currentNumber[number]];
        }
    }

    void DownButtonClicked(int number)
    {
        StartCoroutine(ButtonAudioClick());

        if (currentNumber[number] <= 0)
        {
            currentNumber[number] = 9;
            numberSprites[number].GetComponent<Image>().sprite = numberImages[currentNumber[number]];
        }
        else
        {
            currentNumber[number]--;
            numberSprites[number].GetComponent<Image>().sprite = numberImages[currentNumber[number]];
        }
    }

    void SolveLock()
    {
        if (currentNumber[0] == 4 && currentNumber[1] == 2 && currentNumber[2] == 9 && currentNumber[3] == 3)
        {
            GameManager.instance.solvingLock = false;

            itemButton.SetActive(true);
            lockPanel.gameObject.SetActive(false);
            boxCollider.enabled = false;
            messageText.SetActive(true);

            Cursor.lockState = CursorLockMode.Locked;

            backdoorKey.SetActive(true);

            if(!keyAudioPlaying)
            {
                keyAudioPlaying = true;
                audioSource.PlayOneShot(keyAudioClip);
            }
            

            for (int i = 0; i < numberSprites.Length; i++)
            {
                currentNumber[i] = 0;
                numberSprites[i].GetComponent<Image>().sprite = numberImages[currentNumber[i]];
            }
        }
    }

    public void ChangeSensitivity()
    {
        GameManager.instance.solvingLock = false;        
        Cursor.lockState = CursorLockMode.Locked;
        messageText.SetActive(true);

        for(int i = 0; i < numberSprites.Length; i++)
        {
            currentNumber[i] = 0;
            numberSprites[i].GetComponent<Image>().sprite = numberImages[currentNumber[i]];
        }
    }

    IEnumerator ButtonAudioClick()
    {
        if(!buttonAudioPlaying)
        {
            buttonAudioPlaying = true;
            audioSource.PlayOneShot(buttonAudioClip);
        }

        yield return new WaitForSeconds(0.1f);

        buttonAudioPlaying = false;

        yield return null;
    }
}
