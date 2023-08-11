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
    private float currentHealth;
    private bool isAttacking = false;
    public float MaxHealth => maxHealth;
    public float CurrentHealth => currentHealth;
    
    private NavMeshAgent agent;
    [SerializeField] private Transform Objective;
    private bool hasReachedBeach;
    

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = swimSpeed;
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (isAttacking)
            return;
        if (currentState == MovementState.Swimming && hasReachedBeach) 
        {
            currentState = MovementState.Walking;
            agent.speed = walkSpeed;
        }

        agent.SetDestination(Objective.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Beach"))
        {
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
                StartCoroutine(AttackCycle(other.gameObject.GetComponent<Objective>()));
            }
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log("Enemy has taken" + damage + "Damage");
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
        objective.TakeDamage(attackDamage);
        isAttacking = false;
    }

}