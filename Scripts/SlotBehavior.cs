using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotBehavior : MonoBehaviour
{
    public GameObject itemInSlot;
    public bool slotted = true;

    private void Update()
    {
        if(slotted == true && itemInSlot != null)
        {
            itemInSlot.transform.position = transform.position;
            itemInSlot.transform.rotation = transform.rotation;
        }
        
    }

    public void InsertInSlot(GameObject item)
    {
        itemInSlot = item;
        item.GetComponent<InventoryDrag>().isDragging = false;
        item.GetComponent<InventoryDrag>().slot = gameObject;
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
