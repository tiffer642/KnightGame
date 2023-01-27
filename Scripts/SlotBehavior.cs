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
                    GetComponent<AudioSource>().Play();
                    swordDetectedSlotTwo = false;
                    enableSwordSpawning = false;
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
                    GetComponent<AudioSource>().Play();
                    swordDetectedSlotOne = false;
                    enableSwordSpawning = false;
                    GameObject.Find("Player").GetComponentInChildren<EquipSword>().EquipInSlotTwo(knightSword);
                    if(GameObject.Find("Player").GetComponent<PlayerController>().animator.GetBool("isSheathed") == false && slotNumber == 2)
                    {
                        GameObject.Find("Player").GetComponent<PlayerController>().animator.SetTrigger("Sheath");
                    }
                }
            }
        }
        else if(slotted == false)
        {
            enableSwordSpawning = true;
        }
        
    }

    public void InsertInSlot(GameObject item)
    {
        itemInSlot = item;
        if(item != null)
        {
            item.GetComponent<InventoryDrag>().isDragging = false;
            item.GetComponent<InventoryDrag>().slot = gameObject;
        }
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

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("SwordInv"))
        {
            InsertInSlot(null);
        }
    }
}
