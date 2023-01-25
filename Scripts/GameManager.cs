using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject paticleWallOne;
    public int roomNum;
    public bool inRoomOne = false;



    private void Update()
    {
        switch(roomNum)
        {
            case 1:
                GameObject[] enemiesR1 = GameObject.FindGameObjectsWithTag("EnemyR1");
                inRoomOne = true;

                if (enemiesR1.Length <= 0 && inRoomOne == true)
                {
                    paticleWallOne.SetActive(false);
                }
                break;
        }
    }
}
