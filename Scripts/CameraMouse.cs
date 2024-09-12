using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CameraMouse : MonoBehaviour
{
    public Vector2 turn;
    [SerializeField] float sensitivity;

    //?ì „??
    public GameObject flashLight;    

    void Awake()
    {
        flashLight = GameObject.Find("FlashLight");        
    }

    void Update()
    {        
        MoveMouse();
        MouseDetectObject();
    }

    void MoveMouse()
    {
        //ì¹´ë©”???Œì „
        turn.x += Input.GetAxis("Mouse X") * sensitivity;
        turn.y += Input.GetAxis("Mouse Y") * sensitivity;

        transform.localRotation = Quaternion.Euler(-turn.y, turn.x, 0);

        //ì¹´ë©”???„ì•„???œí•œ
        if (turn.y > 50.0f)
        {
            turn.y = 50.0f;
        }
        else if (turn.y < -50.0f)
        {
            turn.y = -50.0f;
        }

        if (GameManager.instance.startGame && Input.GetKey(KeyCode.Q))
        {
            GameManager.instance.freezeCamera = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else if(GameManager.instance.startGame && Input.GetKeyUp(KeyCode.Q))
        {
            GameManager.instance.freezeCamera = false;
            Cursor.lockState = CursorLockMode.Locked;            
        }

        if(GameManager.instance.solvingLock || GameManager.instance.freezeCamera || GameManager.instance.obtainPaper || GameManager.instance.activeUI)
        {
            sensitivity = 0;
        }
        else if(!GameManager.instance.solvingLock || !GameManager.instance.freezeCamera || !GameManager.instance.obtainPaper || !GameManager.instance.activeUI)
        {
            sensitivity = 1.5f;
        }

        //?ì „???Œì „
        flashLight.transform.localRotation = Quaternion.Euler(-turn.y, turn.x, 0);        
    }

    void MouseDetectObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit rayHit;

        if(Physics.Raycast(ray, out rayHit))
        {
            if (rayHit.transform.CompareTag("Gate") || rayHit.transform.CompareTag("Lock") || rayHit.transform.CompareTag("Key")
                || rayHit.transform.CompareTag("Door") || rayHit.transform.CompareTag("Dumpster") || rayHit.transform.CompareTag("StorageDoor")
                || rayHit.transform.CompareTag("BackDoor") || rayHit.transform.CompareTag("RustedPaper"))
            {
                GameManager.instance.activateUI = true;
            }
            else
            {
                GameManager.instance.activateUI = false;
            }            
        }
    }
}