using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    public bool isPlayerDetected = false;
    public GameObject playerObject;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            isPlayerDetected = true;
            playerObject = other.gameObject;
            Debug.Log("PlayerDetectect");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerDetected = false;
            playerObject = null;
            Debug.Log("PlayerLost");
        }
    }
}
