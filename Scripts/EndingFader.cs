using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingFader : MonoBehaviour
{
    Animator animator;
    public EndingGateController endingGateController;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (endingGateController.gateClose)
        {
            StartCoroutine(FadeOutCouroutine());
        }
    }

    IEnumerator FadeOutCouroutine()
    {
        yield return new WaitForSeconds(3.0f);

        animator.GetComponent<Animator>().SetTrigger("FadeOut");

        yield return new WaitForSeconds(3.0f);
    }
}
