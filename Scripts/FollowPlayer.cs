using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;    

    void Awake()
    {
        player = GameObject.Find("Player");
    }

    void LateUpdate()
    {
        transform.position = player.transform.position;
    }
}
