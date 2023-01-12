using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipSword : MonoBehaviour
{
    public GameObject equipPos1;
    public GameObject inPos1;
    public GameObject equipPos2;
    public GameObject inPos2;
    public GameObject equipPosHand;
    public GameObject inPosHand;
    public GameObject inventory;

    private void Start()
    {
        equipPos1 = GameObject.Find("SwordHipEquipPos1");
        equipPos2 = GameObject.Find("SwordHipEquipPos2");
        equipPosHand = GameObject.Find("SwordHandEquipPos");
        inventory = GameObject.Find("BeltInventory");
    }

    public void EquipInSlotOne(GameObject sword)
    {
        inPos1 = sword;
        //Instantiate(sword, equipPos1.transform.position, equipPos1.transform.rotation);
    }

    public void EquipInSlotTwo(GameObject sword)
    {
        inPos2 = sword;
        //Instantiate(sword, equipPos2.transform.position, equipPos2.transform.rotation);
    }

    public void EquipInHand()
    {
        inPosHand = inPos1;
        //Instantiate(inPosHand, equipPosHand.transform.position, equipPosHand.transform.rotation);
    }
}
