using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 2.0f;

    public GameObject mainCamera;
    public GameObject flashLight;
    public bool turnLight;

    public float axisH;
    public float axisV;

    public AudioSource flashLightAudioSource;
    public AudioClip flashLightAudioClip;

    public AudioSource walkFloorAudioSource;    
    public bool walkFloorAudioPlaying = false;

    public AudioSource runFloorAudioSource;
    public bool runFloorAudioPlaying = false;

    public AudioSource walkGrassAudioSource;
    public bool walkGrassAudioPlaying = false;

    public AudioSource runGrassAudioSource;
    public bool runGrassAudioPlaying = false;

    CameraMouse cameraMouse;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(mainCamera);
        DontDestroyOnLoad(flashLight);

        mainCamera = GameObject.Find("Main Camera");
        flashLight = GameObject.Find("FlashLight");
    }

    void Start()
    {
        turnLight = false;
        flashLight.GetComponent<Light>().enabled = false;
        cameraMouse = GameObject.Find("Main Camera").GetComponent<CameraMouse>();
        flashLightAudioSource = GetComponent<AudioSource>();        
    }

    void Update()
    {
        TurnFlashLight();
        ChangeLocation();

        if(SceneManager.GetActiveScene().name == "Ending")
        {
            Destroy(gameObject);
            Destroy(mainCamera);
            Destroy(flashLight);
        }
    }

    void FixedUpdate()
    {
        axisH = Input.GetAxis("Horizontal");
        axisV = Input.GetAxis("Vertical");        

        //걷기 OR 달리기
        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.Translate(Vector3.forward * axisV * (speed * 2) * Time.deltaTime);
            transform.Translate(Vector3.right * axisH * (speed * 2) * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.forward * axisV * speed * Time.deltaTime);
            transform.Translate(Vector3.right * axisH * speed* Time.deltaTime);
        }

        if (GameManager.instance.isDumpsterOpen || GameManager.instance.isBackDoorOpen || GameManager.instance.solvingLock || GameManager.instance.isRoomDoorOpen)
        {
            speed = 0f;

            axisH = 0;
            axisV = 0;
        }
        else if (!GameManager.instance.isDumpsterOpen || !GameManager.instance.isBackDoorOpen || !GameManager.instance.solvingLock || !GameManager.instance.isRoomDoorOpen)
        {
            speed = 2.0f;

            axisH = Input.GetAxis("Horizontal");
            axisV = Input.GetAxis("Vertical");
        }

        //카메라 회전을 이용하여 플레이어 회전
        transform.localRotation = Quaternion.Euler(0, cameraMouse.turn.x, 0);           
    }

    void TurnFlashLight()
    {
        //손전등 ON/OFF
        if (Input.GetKeyDown(KeyCode.Mouse1) && !turnLight && !GameManager.instance.isFlashLight)
        {
            turnLight = true;
            GameManager.instance.isFlashLight = true;
            flashLight.GetComponent<Light>().enabled = true;

            flashLightAudioSource.PlayOneShot(flashLightAudioClip);
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1) && turnLight && GameManager.instance.isFlashLight)
        {
            turnLight = false;
            GameManager.instance.isFlashLight = false;
            flashLight.GetComponent<Light>().enabled = false;

            flashLightAudioSource.PlayOneShot(flashLightAudioClip);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Grass")
        {
            if ((axisH == 1 || axisH == -1 || axisV == 1 || axisV == -1))
            {
                walkGrassAudioPlaying = true;

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    walkGrassAudioPlaying = false;
                    runGrassAudioPlaying = true;
                }
                else
                {
                    walkGrassAudioPlaying = true;
                    runGrassAudioPlaying = false;
                }
            }
            else if ((axisH == 0 || axisV == 0))
            {
                walkGrassAudioPlaying = false;
                runGrassAudioPlaying = false;
            }

            if (walkGrassAudioPlaying)
            {
                if (!walkGrassAudioSource.isPlaying)
                {
                    walkGrassAudioSource.Play();
                }
            }
            else
            {
                walkGrassAudioSource.Stop();
            }

            if (runGrassAudioPlaying)
            {
                if (!runGrassAudioSource.isPlaying)
                {
                    runGrassAudioSource.Play();
                }
            }
            else
            {
                runGrassAudioSource.Stop();
            }
        }
        else if(collision.gameObject.tag == "Floor")
        {
            if((axisH == 1 || axisH == -1 || axisV == 1 || axisV == -1))
            {
                walkFloorAudioPlaying = true;

                if(Input.GetKey(KeyCode.LeftShift))
                {
                    walkFloorAudioPlaying = false;
                    runFloorAudioPlaying = true;
                }
                else
                {
                    walkFloorAudioPlaying = true;
                    runFloorAudioPlaying = false;
                }
            }
            else if((axisH == 0 || axisV == 0))
            {
                walkFloorAudioPlaying = false;
                runFloorAudioPlaying = false;
            }

            if(walkFloorAudioPlaying)
            {
                if (!walkFloorAudioSource.isPlaying)
                {
                    walkFloorAudioSource.Play();
                }
            }
            else
            {
                walkFloorAudioSource.Stop();
            }

            if(runFloorAudioPlaying)
            {
                if(!runFloorAudioSource.isPlaying)
                {
                    runFloorAudioSource.Play();
                }
            }
            else
            {
                runFloorAudioSource.Stop();
            }
        }
    }

    void ChangeLocation()
    {
        if(SceneManager.GetActiveScene().name == "Room1" && GameManager.instance.enterBuilding)
        {
            transform.position = new Vector3(20.5f, 1.65f, 3.0f);            

            GameManager.instance.enterBuilding = false;
        }
    }    
}
