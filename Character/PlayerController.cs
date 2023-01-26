using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Attacking")]
    public float comboWaitTime;
    public float pushAmount;
    public float health = 100;

    [Header("Inventory System")]
    public GameObject inventory;
    public GameObject grabableSword;
    public bool isInvOpen = false;
    public GameObject mainMenuButton;

    [Header("Weapon Sheathing System")]
    public bool sheathing = false;
    public GameObject swordObject;
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

    [Header("Componants")]
    public Rigidbody rb;
    public Animator animator;
    public GameManager gm;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        gm = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (health > 0)
        {
            gm.isPlayerDead = false;
            if (inventory.activeInHierarchy == true)
            {
                verticalInput = 0;
                horizontalInput = 0;
                rb.velocity = Vector3.zero;
            }
            else
            {
                verticalInput = Input.GetAxis("Vertical");
                horizontalInput = Input.GetAxis("Horizontal");
            }

            if (rb.velocity.magnitude > maxSpeed)
            {
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
            }

            transform.Rotate(Vector3.up, turnAngle * turnSpeed * horizontalInput * Time.deltaTime);

            animator.SetFloat("MovementSpeed", verticalInput);
            animator.SetFloat("HorizontalSpeed", horizontalInput);

            //Sheath Weapon and shield
            if (Input.GetKeyDown(KeyCode.E))
            {
                //Check if items are not sheathed
                if (sheathing == false)
                {
                    Debug.Log("Sheathed");
                    //if items are not sheathed, sheath them
                    sheathing = true;
                    animator.SetTrigger("Sheath");
                }
                else if (sheathing == true && inventory.GetComponentInChildren<SlotBehavior>().slotted == true && inventory.GetComponentInChildren<SlotBehavior>().slotNumber == 1 && animator.GetBool("isSheathed") == true && animator.GetFloat("MovementSpeed") == 0)
                {
                    Debug.Log("Unsheathed");
                    //if items are sheathed, unsheath them
                    sheathing = false;
                    GetComponentInChildren<EquipSword>().EquipInHand(swordObject);
                    shieldUsing.gameObject.SetActive(true);
                    shieldDisplay.gameObject.SetActive(false);
                    animator.SetTrigger("Sheath");
                }
                else if (sheathing == true && inventory.GetComponentInChildren<SlotBehavior>().slotted == true && inventory.GetComponentInChildren<SlotBehavior>().slotNumber == 2)
                {
                    sheathing = false;
                }
                else if (sheathing == true && animator.GetBool("isSheathed") == false)
                {
                    sheathing = false;
                }
                //change animations accordingly to being sheathed and unsheathed
                animator.SetBool("isSheathed", sheathing);

            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                if (isInvOpen == false)
                {
                    isInvOpen = true;
                    inventory.SetActive(true);
                    mainMenuButton.SetActive(true);
                    if (GameObject.Find("SwordSlot").GetComponent<SlotBehavior>().itemInSlot != null)
                    {
                        GameObject.Find("SwordSlot").GetComponent<SlotBehavior>().itemInSlot.SetActive(true);
                    }

                    if (GameObject.Find("SwordSlot (1)").GetComponent<SlotBehavior>().itemInSlot != null)
                    {
                        GameObject.Find("SwordSlot (1)").GetComponent<SlotBehavior>().itemInSlot.SetActive(true);
                    }

                    if (grabableSword != null)
                    {
                        grabableSword.SetActive(true);
                    }
                }
                else if (isInvOpen == true)
                {
                    if (inventory.GetComponentInChildren<SlotBehavior>().slotted == true && grabableSword != null)
                    {
                        grabableSword.SetActive(false);
                    }
                    isInvOpen = false;
                    if (GameObject.Find("SwordSlot").GetComponent<SlotBehavior>().itemInSlot != null)
                    {
                        GameObject.Find("SwordSlot").GetComponent<SlotBehavior>().itemInSlot.SetActive(false);
                    }

                    if (GameObject.Find("SwordSlot (1)").GetComponent<SlotBehavior>().itemInSlot != null)
                    {
                        GameObject.Find("SwordSlot (1)").GetComponent<SlotBehavior>().itemInSlot.SetActive(false);
                    }

                    inventory.SetActive(false);
                    mainMenuButton.SetActive(false);
                }
            }

            if (Input.GetMouseButtonDown(0) && inventory.activeInHierarchy == false && sheathing == false)
            {
                if (animator.GetBool("IsAttacking") == false)
                {
                    animator.SetBool("IsAttacking", true);
                    animator.SetTrigger("Attack1");
                    StartCoroutine(ComboDelay());
                }
                else if (animator.GetBool("IsAttacking") == true)
                {
                    animator.SetTrigger("Attack1");
                    StartCoroutine(ComboDelay());
                }
            }
        }
        else if(health <= 0)
        {
            animator.SetTrigger("Death");
            gm.isPlayerDead = true;
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
        Debug.Log("SheathedAnimation");
        if(sheathing == true && inventory.GetComponentInChildren<SlotBehavior>().slotted == true && inventory.GetComponentInChildren<SlotBehavior>().slotNumber == 1)
        {
            GetComponentInChildren<EquipSword>().EquipInSlotOne(swordObject);
        }
        shieldUsing.gameObject.SetActive(false);
        shieldDisplay.gameObject.SetActive(true);
    }

    IEnumerator ComboDelay()
    {
        yield return new WaitForSeconds(comboWaitTime);
        animator.SetBool("IsAttacking", false);
    }

    public void PushWhileAttacking()
    {
        rb.AddForce(transform.forward * pushAmount, ForceMode.Impulse);
    }

    public void EnableCollider()
    {
        swordObject.GetComponent<BoxCollider>().enabled = true;
    }

    public void DisableCollider()
    {
        swordObject.GetComponent<BoxCollider>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Room1"))
        {
            gm.roomNum = 1;
        }

        if(other.CompareTag("EnemyWeapon"))
        {
            health -= 10;
        }
    }
}