using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeleController : MonoBehaviour
{
    [Header("Other")]

    public Animator animator;
    public float health = 100;
    public bool canBeHit = true;
    public float immunityFrameCount = 3f;

    [Header("NavMesh")]
    public NavMeshAgent agent;
    public float moveSpeed;

    private void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if(health <= 0)
        {
            Invoke("StartDeath", 0);
        }

        if(GetComponentInChildren<DetectPlayer>().isPlayerDetected == true && GetComponentInChildren<DetectPlayer>().playerObject != null)
        {
            agent.destination = GetComponentInChildren<DetectPlayer>().playerObject.transform.position;
            agent.speed = moveSpeed;
        }
        else
        {
            agent.speed = 0;
        }

        animator.SetFloat("MovementSpeed", agent.speed);
    }

    public void DespawnSkele()
    {
        Destroy(gameObject);
    }

    public void StartDeath()
    {
        animator.SetTrigger("Death");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(canBeHit == true)
        {
            if (other.CompareTag("Weapon"))
            {
                animator.SetTrigger("Hit");
                health -= 25;
            }
            canBeHit = false;
            StartCoroutine(ImmunityFrames());
        }
    }

    IEnumerator ImmunityFrames()
    {
        yield return new WaitForSeconds(immunityFrameCount);
        canBeHit = true;
    }
}
