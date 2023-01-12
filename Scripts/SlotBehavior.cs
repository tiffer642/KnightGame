using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotBehavior : MonoBehaviour
{
    [Header("Weapons")]
    public GameObject knightSword;

    [Header("Other")]
    public bool swordDetectedSlotOne = false;
    public bool swordDetectedSlotTwo = false;
    public bool enableSwordSpawning = true;
    public GameObject itemInSlot;
    public bool slotted = false;
    public int slotNumber;

    private void Update()
    {
        if(slotted == true && itemInSlot != null)
        {
            itemInSlot.transform.position = transform.position;
            itemInSlot.transform.rotation = transform.rotation;



            if (itemInSlot.name.Equals(knightSword.GetComponent<TypeOfWeapon>().typeOfWeapon) && slotNumber == 1)
            {
                if(swordDetectedSlotOne == false)
                {
                    swordDetectedSlotOne = true;
                }
                else if(swordDetectedSlotOne == true && enableSwordSpawning == true)
                {
                    swordDetectedSlotTwo = false;
                    enableSwordSpawning = false;
                    Debug.Log("SwordSpawnedInSlot1");
                    Debug.Log("What is it trying to spawn: " + knightSword);
                    GameObject.Find("Player").GetComponentInChildren<EquipSword>().EquipInSlotOne(knightSword);
                }
            }
            else if (itemInSlot.name == knightSword.GetComponent<TypeOfWeapon>().typeOfWeapon && slotNumber == 2)
            {
                if(swordDetectedSlotTwo == false)
                {
                    swordDetectedSlotTwo = true;
                }
                else if(swordDetectedSlotTwo == true && enableSwordSpawning == true)
                {
                    swordDetectedSlotOne = false;
                    enableSwordSpawning = false;
                    Debug.Log("SwordSpawnedInSlot2");
                    Debug.Log("What is it trying to spawn: " + knightSword);
                    GameObject.Find("Player").GetComponentInChildren<EquipSword>().EquipInSlotTwo(knightSword);
                }
            }
        }
        else
        {
            enableSwordSpawning = true;
        }
        
    }

    public void InsertInSlot(GameObject item)
    {
        itemInSlot = item;
        item.GetComponent<InventoryDrag>().isDragging = false;
        item.GetComponent<InventoryDrag>().slot = gameObject;
        GameObject.Find("Player").GetComponent<PlayerController>().grabableSword = item;
        slotted = true;

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("SwordInv"))
        {
            InsertInSlot(other.gameObject);
        }
    }
}
