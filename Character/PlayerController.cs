using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Weapon System")]
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
        verticalInput = Input.GetAxisRaw("Vertical");
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        }

        transform.Rotate(Vector3.up, turnAngle * turnSpeed * horizontalInput * Time.deltaTime);

        animator.SetFloat("MovementSpeed", verticalInput);
        animator.SetFloat("HorizontalSpeed", horizontalInput);

        if(Input.GetKeyDown(KeyCode.E))
        {
            if(sheathing == false)
            {
                sheathing = true;
            }
            else
            {
                sheathing = false;
                swordUsing.gameObject.SetActive(true);
                swordDisplay.gameObject.SetActive(false);
                shieldUsing.gameObject.SetActive(true);
                shieldDisplay.gameObject.SetActive(false);
            }

            animator.SetTrigger("Sheath");
            animator.SetBool("isSheathed", sheathing);
            
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
        swordUsing.gameObject.SetActive(false);
        swordDisplay.gameObject.SetActive(true);
        shieldUsing.gameObject.SetActive(false);
        shieldDisplay.gameObject.SetActive(true);
    }
}