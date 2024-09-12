using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFader : MonoBehaviour
{
    public Image fadePanel;
    Animator fadePanelAnimator;

    void Start()
    {
        fadePanelAnimator = fadePanel.GetComponent<Animator>();
    }
    
    void Update()
    {
        if(GameManager.instance.isBackDoorOpen || GameManager.instance.isRoomDoorOpen)
        {
            fadePanelAnimator.SetTrigger("FadeOut");
        }
    }
}
