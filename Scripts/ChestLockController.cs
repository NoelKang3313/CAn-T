using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestLockController : MonoBehaviour
{
    public Image FKeyImage;

    BoxCollider boxCollider;
    public GameObject messageText;

    public Image lockPanel;
    public GameObject itemButton;

    [SerializeField]
    int[] currentNumber = { 0, 0, 0, 0 };

    public Image[] numberSprites;
    public Button[] buttons;
    public Sprite[] numberImages;

    AudioSource audioSource;
    public AudioClip buttonAudioClip;
    public bool buttonAudioPlaying = false;

    public AudioClip chestOpenAudioClip;
    public bool chestOpenAudioPlaying;

    public GameObject chestLid;
    public float rotSpeed = 20.0f;

    public GameObject drawerKey;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();

        audioSource = GetComponent<AudioSource>();

        for (int i = 0; i < buttons.Length; i++)
        {
            int number = i;
            buttons[i].onClick.AddListener(() => ButtonClicked(number));
        }
    }

    void Update()
    {
        SolveLock();
    }

    void ButtonClicked(int number)
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

    public void ChangeSensitivity()
    {
        GameManager.instance.solvingLock = false;
        Cursor.lockState = CursorLockMode.Locked;
        messageText.SetActive(true);

        for (int i = 0; i < numberSprites.Length; i++)
        {
            currentNumber[i] = 0;
            numberSprites[i].GetComponent<Image>().sprite = numberImages[currentNumber[i]];
        }
    }

    void SolveLock()
    {
        if (currentNumber[0] == 6 && currentNumber[1] == 9 && currentNumber[2] == 5 && currentNumber[3] == 2)
        {
            if(!chestOpenAudioPlaying)
            {
                chestOpenAudioPlaying = true;
                audioSource.PlayOneShot(chestOpenAudioClip);
            }

            GameManager.instance.solvingLock = false;

            chestLid.SetActive(false);
            drawerKey.SetActive(true);

            itemButton.SetActive(true);
            lockPanel.gameObject.SetActive(false);
            boxCollider.enabled = false;
            messageText.SetActive(true);

            Cursor.lockState = CursorLockMode.Locked;

            for (int i = 0; i < numberSprites.Length; i++)
            {
                currentNumber[i] = 0;
                numberSprites[i].GetComponent<Image>().sprite = numberImages[currentNumber[i]];
            }
        }
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

            if (GameManager.instance.solvingLock)
            {
                FKeyImage.gameObject.SetActive(false);
            }
        }
        else if (other.gameObject.CompareTag("Player") && !GameManager.instance.activateUI)
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

    IEnumerator ButtonAudioClick()
    {
        if (!buttonAudioPlaying)
        {
            buttonAudioPlaying = true;
            audioSource.PlayOneShot(buttonAudioClip);
        }

        yield return new WaitForSeconds(0.1f);

        buttonAudioPlaying = false;

        yield return null;
    }
}
