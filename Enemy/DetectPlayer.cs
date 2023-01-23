using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    public bool isPlayerDetected = false;
    public GameObject playerObject;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isPlayerDetected = true;
            playerObject = collision.gameObject;
        }
    }
}
