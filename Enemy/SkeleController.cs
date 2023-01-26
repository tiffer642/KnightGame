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
    public GameObject detector;
    public GameObject distanceChecker;
    public bool canAttack = true;
    public GameObject self;
    public GameObject hitCollition;

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

        if (detector.GetComponent<DetectPlayer>().isPlayerDetected == true && detector.GetComponent<DetectPlayer>().playerObject != null && health > 0)
        {
            agent.destination = detector.GetComponent<DetectPlayer>().playerObject.transform.position;

            if (distanceChecker.GetComponent<KeepDistance>().keepDistance == true)
            {
                agent.speed = 0;
                transform.LookAt(detector.GetComponent<DetectPlayer>().playerObject.transform.position);
                animator.SetBool("IsAttacking", true);
                StartCoroutine("Attacking");
                if (canAttack == true)
                {
                    animator.SetTrigger("Attack");
                }
            }
            else if (distanceChecker.GetComponent<KeepDistance>().keepDistance == false)
            {
                agent.speed = moveSpeed;
                animator.SetBool("IsAttacking", false);
            }
        }
        else
        {
            agent.speed = 0;
            animator.SetBool("IsAttacking", false);
        }

        animator.SetFloat("MovementSpeed", agent.speed);
    }

    public void DespawnSkele()
    {
        Destroy(self);
        Destroy(detector);
        Destroy(distanceChecker);
    }

    public void StartDeath()
    {
        agent.speed = 0;
        animator.SetTrigger("Death");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(canBeHit == true)
        {
            if (other.CompareTag("Weapon"))
            {
                animator.SetTrigger("Hit");
                agent.speed = 0;
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

    IEnumerable Attacking()
    {
        yield return new WaitForSeconds(4);
        if(canBeHit == true)
        {
            canAttack = true;
        }
        if(canBeHit == false)
        {
            canAttack = false;
        }
    }

    public void EnableHitCollitions()
    {
        hitCollition.SetActive(true);
    }

    public void DisableHitCollitions()
    {
        hitCollition.SetActive(false);
    }
}
