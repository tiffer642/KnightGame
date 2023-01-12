using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Inventory System")]
    public GameObject inventory;
    public GameObject grabableSword;
    public bool isInvOpen = false;

    [Header("Weapon Sheathing System")]
    public bool sheathing = false;
    public GameObject swordUsing;
    public GameObject swordDisplay;
    public GameObject shieldUsing;
    public GameObject shieldDisplay;

    [Header("MovementVariables")]
    public float moveSpeed;
    public float maxSpeed;
    public float verticalInput;
    public float horizontalInput;
    public Vector3 movementDirection;

    public float turnSpeed;
    public float turnAngle;

    private bool moving;

    [Header("Componants")]
    public Rigidbody rb;
    public Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        swordDisplay = GetComponentInChildren<EquipSword>().inPos1;
        swordUsing = GetComponentInChildren<EquipSword>().inPosHand;

        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        }

        transform.Rotate(Vector3.up, turnAngle * turnSpeed * horizontalInput * Time.deltaTime);

        animator.SetFloat("MovementSpeed", verticalInput);
        animator.SetFloat("HorizontalSpeed", horizontalInput);

        //Sheath Weapon and shield
        if(Input.GetKeyDown(KeyCode.E))
        {
            //Check if items are not sheathed
            if(sheathing == false)
            {
                //if items are not sheathed, sheath them
                sheathing = true;
            }
            else
            {
                //if items are sheathed, unsheath them
                sheathing = false;
                GetComponentInChildren<EquipSword>().EquipInHand();

                //check if sword display and sword using are empty
                if(swordDisplay != null && swordUsing != null)
                {
                    //if not empty activate sword using, deactivate sword display
                    swordUsing.gameObject.SetActive(true);
                    swordDisplay.gameObject.SetActive(false);
                }
                shieldUsing.gameObject.SetActive(true);
                shieldDisplay.gameObject.SetActive(false);
            }
            //change animations accordingly to being sheathed and unsheathed
            animator.SetTrigger("Sheath");
            animator.SetBool("isSheathed", sheathing);
            
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isInvOpen == false)
            {
                isInvOpen = true;
                inventory.SetActive(true);
                if(grabableSword != null)
                {
                    grabableSword.SetActive(true);
                }
            }
            else if(isInvOpen == true)
            {
                if (inventory.GetComponentInChildren<SlotBehavior>().slotted == true && grabableSword != null)
                {
                    grabableSword.SetActive(false);
                }
                isInvOpen = false;
                inventory.SetActive(false);
            }
        }
    }

    void FixedUpdate()
    {
        if(verticalInput == 1)
        {
            rb.AddForce(transform.forward * moveSpeed, ForceMode.Force);
        }
        else if (verticalInput == -1)
        {
            rb.AddForce(transform.forward * moveSpeed * -1, ForceMode.Force);
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    public void SheathingIn()
    {
        if (swordDisplay != null && swordUsing != null)
        {
            swordUsing.gameObject.SetActive(false);
            swordDisplay.gameObject.SetActive(true);
        }
        shieldUsing.gameObject.SetActive(false);
        shieldDisplay.gameObject.SetActive(true);
    }
}