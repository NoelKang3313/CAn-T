using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FlagText : MonoBehaviour
{
    public TextMeshPro[] flagText = new TextMeshPro[4];
    
    void Update()
    {
        for(int i = 0; i < flagText.Length; i++)
        {
            if(GameManager.instance.isFlashLight)
            {
                flagText[i].gameObject.SetActive(false);
            }
            else
            {
                flagText[i].gameObject.SetActive(true);
            }
        }
    }
}
