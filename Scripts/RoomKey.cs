using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomKey : MonoBehaviour
{
    public Image FKeyImage;
    public Image keyImage;

    UIRoom1 uIRoom;

    void Start()
    {
        uIRoom = GameObject.Find("UIRoom1").GetComponent<UIRoom1>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && GameManager.instance.activateUI)
        {
            FKeyImage.gameObject.SetActive(true);

            if (Input.GetKey(KeyCode.F))
            {
                GameManager.instance.getRoomKey = true;
                gameObject.SetActive(false);
                FKeyImage.gameObject.SetActive(false);

                Color imageColor = keyImage.color;
                imageColor.a = 1.0f;
                keyImage.color = imageColor;

                uIRoom.buttonActive[3] = true;
            }
        }
        else if (other.gameObject.CompareTag("Player") && !GameManager.instance.activateUI)
        {
            FKeyImage.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            FKeyImage.gameObject.SetActive(false);
        }
    }
}
