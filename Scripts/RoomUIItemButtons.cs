using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RoomUIItemButtons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image itemNameUI;
    public int indexNum;

    UIRoom1 uIRoom1;

    void Start()
    {
        uIRoom1 = GameObject.Find("UIRoom1").GetComponent<UIRoom1>();
    }       

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (uIRoom1.buttonActive[indexNum])
        {
            itemNameUI.gameObject.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventeData)
    {
        if (uIRoom1.buttonActive[indexNum])
        {
            itemNameUI.gameObject.SetActive(false);
        }
    }
}
