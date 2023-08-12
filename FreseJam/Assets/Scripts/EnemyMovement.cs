using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour, IHealth
{
    public enum MovementState
    {
        Swimming,
        Walking
    }

    private MovementState currentState = MovementState.Swimming;
    [SerializeField] private float attackDamage = 10f; 
    [SerializeField] private float attackDelay = 2f;
    [SerializeField] private float swimSpeed = 3f;
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private GameObject blood;
    [SerializeField] private Transform particlesDiePosition;
    private float currentHealth;
    private bool isAttacking = false;
    public float MaxHealth => maxHealth;
    public float CurrentHealth => currentHealth;
    
    private NavMeshAgent agent;
    private Objective currentObjective;
    private bool hasReachedBeach;

    [SerializeField] private Animator anim;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = swimSpeed;
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (isAttacking)
        {
            return;
        }

        if (currentState == MovementState.Swimming && hasReachedBeach) 
        {
            currentState = MovementState.Walking;
            agent.speed = walkSpeed;
        }
        
        if (!currentObjective || !currentObjective.gameObject.activeSelf)
        {
            UpdateTarget();    
        }

        if (currentObjective)
            agent.SetDestination(currentObjective.transform.position);
        else
            agent.isStopped = true;

        if (agent.velocity.magnitude > 0.1f)
        {
            anim.SetBool("isEating", false);
        }
    }

    private void UpdateTarget()
    {
        currentObjective = GameManager.Instance.AssignRandomObjective();
        anim.SetBool("isEating", false);
        if (currentObjective)
        {
            agent.SetDestination(currentObjective.transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Beach"))
        {
            anim.SetBool("isWalking", true);
            hasReachedBeach = true;
            //Adjust Walkspeed
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Objective"))
        {
            if (!isAttacking)
            {
                agent.isStopped = true;
                isAttacking = true;
                anim.SetBool("isEating", true);
                StartCoroutine(AttackCycle(other.gameObject.GetComponent<Objective>()));
            }
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }
    
    private IEnumerator AttackCycle(Objective objective)
    {
        yield return new WaitForSeconds(attackDelay);
        _audioSource.Play();
        Instantiate(blood, particlesDiePosition.position, particlesDiePosition.rotation);
        //Add Eating Sound + Vfx?
        objective.TakeDamage(attackDamage);
        agent.isStopped = false;
        isAttacking = false;
    }

}