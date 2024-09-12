using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackDoorKey : MonoBehaviour
{
    public Image FKeyImage;
    public Image keyImage;

    UIEntrance uIEntrance;

    void Start()
    {
        uIEntrance = GameObject.Find("UIEntrance").GetComponent<UIEntrance>();
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && GameManager.instance.activateUI)
        {
            FKeyImage.gameObject.SetActive(true);

            if (Input.GetKey(KeyCode.F))
            {                
                GameManager.instance.getBackDoorKey = true;
                gameObject.SetActive(false);
                FKeyImage.gameObject.SetActive(false);

                Color imageColor = keyImage.color;
                imageColor.a = 1.0f;
                keyImage.color = imageColor;

                uIEntrance.buttonActive[2] = true;
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
}
