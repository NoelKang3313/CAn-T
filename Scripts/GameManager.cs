using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool startGame = false;      //게임시작 버튼 클릭 및 게임시작 확인    
    public bool enterBuilding = false;
    public bool enterNextRoom = false;

    public bool activateUI = false;

    public bool activeUI = false;

    public bool freezeCamera = false;

    //SoundManager용 Boolean
    public bool isDoorOpen = false;
    public bool isDumpsterOpen = false;
    public bool isEntranceGateOpen = false;
    public bool isBackDoorOpen = false;
    public bool isCabinetDoorOpen = false;
    public bool isRoomDoorOpen = false;

    public bool solvingLock = false;        //LockPanel 열렸을 때, FKeyUI를 비활성화용 Boolean

    public bool obtainPaper = false;

    public bool getStorageKey = false;
    public bool getBackDoorKey = false;
    public bool getCabinetKey = false;
    public bool getRoomKey = false;

    public bool isFlashLight = false;

    public static GameManager instance;

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);        
    }

    void Update()
    {
        MouseVisible();

        if (enterBuilding)
        {
            LoadScene("Room1");            
        }

        if(enterNextRoom)
        {
            enterNextRoom = false;
            LoadScene("Ending");
        }
    }

    //마우스 중앙으로 위치 설정
    public void SetMouse()
    {
        if (!startGame)
        {
            startGame = true;

            Cursor.lockState = CursorLockMode.Locked;            
        }
    }

    //게임플레이 동안 마우스 보여주기
    void MouseVisible()
    {
        if(startGame && Input.GetKey(KeyCode.Q))
        {
            freezeCamera = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else if(startGame && Input.GetKeyUp(KeyCode.Q))
        {
            freezeCamera = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
