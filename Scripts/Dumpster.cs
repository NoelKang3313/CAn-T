using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dumpster : MonoBehaviour
{
    public float rotSpeed = 20.0f;

    public GameObject leftLid;
    public GameObject rightLid;

    public Image FKeyImage;
    BoxCollider boxCollider;

    public GameObject player;

    public GameObject blackCat;
    public float catSpeed = 20.0f;
    Rigidbody catRigid;

    AudioSource audioSource;

    public AudioClip dumpsterAudioClip;
    public bool dumpsterAudioPlaying = false;

    public AudioClip catAudioClip;
    public bool catAudioPlaying = false;

    public AudioClip keyAudioClip;
    public bool keyAudioPlaying = false;

    public GameObject storageKey;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        catRigid = blackCat.GetComponent<Rigidbody>();

        audioSource = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        RotateLid();        
    }

    void RotateLid()
    {
        if(GameManager.instance.isDumpsterOpen)
        {
            if(!dumpsterAudioPlaying)
            {
                dumpsterAudioPlaying = true;
                audioSource.PlayOneShot(dumpsterAudioClip);
            }

            leftLid.transform.Rotate(-Vector3.right * rotSpeed * Time.deltaTime);
            rightLid.transform.Rotate(-Vector3.right * rotSpeed * Time.deltaTime);

            FKeyImage.gameObject.SetActive(false);
            boxCollider.enabled = false;

            blackCat.transform.LookAt(player.transform);

            if (leftLid.transform.rotation.x <= -0.45f && rightLid.transform.rotation.x <= -0.45f)
            {
                GameManager.instance.isDumpsterOpen = false;
                rotSpeed = 0f;

                StartCoroutine(DestroyCat());
                StopCoroutine(DestroyCat());
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && GameManager.instance.activateUI)
        {
            FKeyImage.gameObject.SetActive(true);

            if(Input.GetKey(KeyCode.F))
            {
                GameManager.instance.isDumpsterOpen = true;
            }
        }
        else if (other.gameObject.CompareTag("Player") && !GameManager.instance.activateUI)
        {
            FKeyImage.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            FKeyImage.gameObject.SetActive(false);
        }
    }

    IEnumerator DestroyCat()
    {
        if (leftLid.transform.rotation.x <= -0.45f && rightLid.transform.rotation.x <= -0.45f)
        {
            catRigid.AddForce(blackCat.transform.forward * catSpeed, ForceMode.Impulse);
            CatAudio();

            yield return new WaitForSeconds(2.0f);

            Destroy(blackCat);
            storageKey.SetActive(true);

            if(!keyAudioPlaying)
            {
                keyAudioPlaying = true;
                audioSource.PlayOneShot(keyAudioClip, 1.0f);
            }

            yield return null;
        }        
    }

    void CatAudio()
    {
        if(!catAudioPlaying)
        {
            catAudioPlaying = true;
            audioSource.PlayOneShot(catAudioClip);
        }
    }
}
