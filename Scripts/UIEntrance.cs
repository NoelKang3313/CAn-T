using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEntrance : MonoBehaviour
{
    public GameObject controlUI;
    public bool controlUIActive = false;

    public GameObject mouseVisibleUI;
    public bool mouseVisibleUIActive = false;

    public GameObject buttons;

    public Image paperUI;

    public Button[] itemButtons;
    public bool[] buttonActive;

    void Start()
    {
        for (int i = 0; i < itemButtons.Length; i++)
        {
            int number = i;
            itemButtons[i].onClick.AddListener(() => ItemButtonClicked(number));
        }
    }    

    void ItemButtonClicked(int number)
    {
        if (buttonActive[number])
        {
            if(number == 0 || number == 2)
            {
                Debug.Log("Key");
            }
            else
            {
                paperUI.gameObject.SetActive(true);

                buttons.SetActive(false);

                GameManager.instance.activeUI = true;
            }
        }
    }

    void Update()
    {
        StartCoroutine(ShowControlUI());

        if (GameManager.instance.getStorageKey)
        {
            StartCoroutine(MouseVisibleUI());
        }

        if(GameManager.instance.activeUI)
        {
            Cursor.lockState = CursorLockMode.None;
        }

        if(GameManager.instance.isBackDoorOpen)
        {
            buttons.SetActive(false);
        }
    }

    IEnumerator ShowControlUI()
    {
        if (!controlUIActive)
        {
            controlUIActive = true;

            yield return new WaitForSeconds(3.0f);

            controlUI.SetActive(true);

            yield return new WaitForSeconds(10.0f);

            controlUI.SetActive(false);
        }
    }

    IEnumerator MouseVisibleUI()
    {
        if(!mouseVisibleUIActive)
        {
            mouseVisibleUIActive = true;

            yield return new WaitForSeconds(1.0f);

            mouseVisibleUI.SetActive(true);

            yield return new WaitForSeconds(10.0f);

            mouseVisibleUI.SetActive(false);
        }
    }
}
