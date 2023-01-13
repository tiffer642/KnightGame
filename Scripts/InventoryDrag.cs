using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDrag : MonoBehaviour
{
    public Rigidbody rb;
    public bool isDragging = false;
    public GameObject slot;
    public float distance;

    void Update()
    {
        if(isDragging == true)
        {
            rb.position =
                Camera.main.ScreenToWorldPoint(new Vector3
                (Input.mousePosition.x, Input.mousePosition.y, -Camera.main.nearClipPlane + distance));
            rb.isKinematic = true;

            GameObject.Find("Player").GetComponentInChildren<EquipSword>().DeleteAll();
        }
        else
        {
            rb.isKinematic = false;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }

    private void OnMouseOver()
    {
        if(Input.GetMouseButton(0))
        {
            isDragging = true;
            if(slot != null)
            {
                slot.GetComponent<SlotBehavior>().slotted = false;
            }
        }
    }
}
