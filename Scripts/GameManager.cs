using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject paticleWallOne;
    public int roomNum;
    public bool inRoomOne = false;

    public GameObject retry;
    public GameObject giveUp;
    public bool isPlayerDead;



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

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Tutorial"))
        {
            if (isPlayerDead == true)
            {
                retry.SetActive(true);
                giveUp.SetActive(true);
            }
            else if (isPlayerDead == false)
            {
                retry.SetActive(false);
                giveUp.SetActive(false);
            }
        }
    }

    public void LoadTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
