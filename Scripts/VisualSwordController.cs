using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualSwordController : MonoBehaviour
{
    public GameObject slot1;
    public bool usingSlot1;
    public GameObject slot2;
    public bool usingSlot2;
    public GameObject slotHand;
    public bool usingSlotHand;

    private void Update()
    {
        if(usingSlot1 == true && usingSlot2 == false && usingSlotHand == false)
        {
            Debug.Log("Slot One");
            transform.position = slot1.transform.position;
            transform.rotation = slot1.transform.rotation;
        }
        else if(usingSlot2 == true && usingSlot1 == false && usingSlotHand == false)
        {
            Debug.Log("Slot Two");
            transform.position = slot2.transform.position;
            transform.rotation = slot2.transform.rotation;
        }
        else if(usingSlotHand == true && usingSlot1 == false && usingSlot2 == false)
        {
            Debug.Log("Slot Hand");
            transform.position = slotHand.transform.position;
            transform.rotation = slotHand.transform.rotation;
        }
    }

    public void SlotOne()
    {
        usingSlot1 = true;
        usingSlot2 = false;
        usingSlotHand = false;
    }

    public void SlotTwo()
    {
        usingSlot1 = false;
        usingSlot2 = true;
        usingSlotHand = false;
    }

    public void SlotHand()
    {
        usingSlot1 = false;
        usingSlot2 = false;
        usingSlotHand = true;
    }
}
