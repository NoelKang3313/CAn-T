using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartFadeInOut : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();        
    }
    
    void Update()
    {
        if(GameManager.instance.startGame)
        {
            StartCoroutine(FadeOutCouroutine());
        }
    }

    IEnumerator FadeOutCouroutine()
    {
        animator.GetComponent<Animator>().SetTrigger("FadeOut");

        yield return new WaitForSeconds(3.0f);

        GameManager.instance.LoadScene("Entrance");        
    }
}
