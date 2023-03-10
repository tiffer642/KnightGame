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
        inPos1 = Instantiate(sword, equipPos1.transform.position, equipPos1.transform.rotation);
        inPos1.GetComponent<VisualSwordController>().slot1 = equipPos1;
        GameObject.Find("Player").GetComponent<PlayerController>().swordObject = inPos1;
        inPos1.GetComponent<VisualSwordController>().SlotOne();
        Destroy(inPos2);
        Destroy(inPosHand);
    }

    public void EquipInSlotTwo(GameObject sword)
    {
        inPos2 = Instantiate(sword, equipPos2.transform.position, equipPos2.transform.rotation);
        inPos2.GetComponent<VisualSwordController>().slot2 = equipPos2;
        GameObject.Find("Player").GetComponent<PlayerController>().swordObject = inPos2;
        inPos2.GetComponent<VisualSwordController>().SlotTwo();
        Destroy(inPos1);
        Destroy(inPosHand);
    }

    public void EquipInHand(GameObject sword)
    {
        inPosHand = Instantiate(sword, equipPosHand.transform.position, equipPosHand.transform.rotation);
        GameObject.Find("Player").GetComponent<PlayerController>().swordObject = inPosHand;
        inPosHand.GetComponent<VisualSwordController>().slotHand = equipPosHand;
        inPosHand.GetComponent<VisualSwordController>().SlotHand();
        Destroy(inPos1);
        Destroy(inPos2);
    }

    public void DeleteAll()
    {
        Destroy(inPos1);
        Destroy(inPos2);
        Destroy(inPosHand);
    }
}
