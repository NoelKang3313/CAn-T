using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GateController : MonoBehaviour
{
    public float rotSpeed = 25.0f;

    public GameObject leftGate;
    public GameObject rightGate;


    public Image FKeyImage;
    BoxCollider boxCollider;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    void Update()
    {
        RotateGate();
    }    

    void RotateGate()
    {
        if(GameManager.instance.isEntranceGateOpen)
        {
            leftGate.transform.Rotate(-Vector3.up * rotSpeed * Time.deltaTime);
            rightGate.transform.Rotate(Vector3.up * rotSpeed * Time.deltaTime);

            FKeyImage.gameObject.SetActive(false);
            boxCollider.enabled = false;

            if (leftGate.transform.rotation.y <= -0.9f && rightGate.transform.rotation.y >= 0.9f)
            {
                GameManager.instance.isEntranceGateOpen = false;

                rotSpeed = 0f;
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && GameManager.instance.activateUI)
        {
            FKeyImage.gameObject.SetActive(true);

            if (Input.GetKey(KeyCode.F))
            {
                GameManager.instance.isEntranceGateOpen = true;
            }            
        }
        else if(other.gameObject.CompareTag("Player") && !GameManager.instance.activateUI)
        {
            FKeyImage.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {            
            FKeyImage.gameObject.SetActive(false);
        }
    }
}
