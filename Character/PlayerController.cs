using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
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
}
