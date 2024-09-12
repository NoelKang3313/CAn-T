using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIItemButtons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image itemNameUI;
    public int indexNum;

    UIEntrance uIEntrance;    

    void Start()
    {
        uIEntrance = GameObject.Find("UIEntrance").GetComponent<UIEntrance>();       
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (uIEntrance.buttonActive[indexNum])
        {
            itemNameUI.gameObject.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (uIEntrance.buttonActive[indexNum])
        {
            itemNameUI.gameObject.SetActive(false);
        }
    }
}
