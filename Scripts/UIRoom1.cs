using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRoom1 : MonoBehaviour
{
    public GameObject buttons;

    public Image braillePaperUI;
    public Image roomkeyPaperUI;

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
            if (number == 1 || number == 3)
            {
                Debug.Log("Key");
            }
            else if(number == 0)
            {
                braillePaperUI.gameObject.SetActive(true);

                buttons.SetActive(false);

                GameManager.instance.activeUI = true;
            }
            else
            {
                roomkeyPaperUI.gameObject.SetActive(true);

                buttons.SetActive(false);

                GameManager.instance.activeUI = true;
            }
        }
    }

    void Update()
    {
        if (GameManager.instance.activeUI)
        {
            Cursor.lockState = CursorLockMode.None;
        }

        if(GameManager.instance.isRoomDoorOpen)
        {
            buttons.SetActive(false);
        }
    }
}
