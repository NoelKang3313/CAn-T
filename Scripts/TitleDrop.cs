using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleDrop : MonoBehaviour
{
    public StartGateController startGateController;
    Rigidbody titleRigidbody;

    void Start()
    {
        titleRigidbody = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        if(startGateController.gateFullyOpened)
        {
            titleRigidbody.isKinematic = false;
        }
    }
}
