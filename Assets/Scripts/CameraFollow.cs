using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject Player;
    public Vector3 offset;

    private void Awake()
    {
        Player = GameObject.FindWithTag("Player");
    }


    private void LateUpdate()
    {
        transform.position = Player.transform.position + offset;
    }
}