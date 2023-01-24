using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public GameObject follow;

    void Update()
    {
        transform.position = follow.transform.position;
        transform.rotation = follow.transform.rotation;
    }
}
