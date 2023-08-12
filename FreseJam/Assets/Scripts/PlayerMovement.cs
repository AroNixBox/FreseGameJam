using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using Random = UnityEngine.Random;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float attackRange = 2.0f;
    [SerializeField] private float attackDamage = 10f;
    [SerializeField] private float attackDelay = 1.0f;
    [SerializeField] private Camera cam;

    [SerializeField] private Animator anim;
    private bool isAttacking = false;
    
    [SerializeField] private AudioSource[] attackSources;
    
    private NavMeshAgent agent;
    private Transform enemyTarget;
    private IHealth enemyHealth;
    private bool isChasingEnemy = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        HandleInput();
        if (agent.velocity.magnitude < 0.1f && !isAttacking)
        {
            anim.SetBool("isRunning", false);
            anim.SetBool("isAttacking", false);
            return;
        }
        HandleCombat();


        if (agent.velocity.magnitude > 0.1f)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 5f);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {
                EnemyMovement enemy = hit.collider.GetComponent<EnemyMovement>();
                if (enemy)
                {
                    
                    enemyTarget = hit.collider.transform;
                    enemyHealth = hit.collider.GetComponent<IHealth>();
                    agent.SetDestination(enemyTarget.position);
                    isChasingEnemy = true;
                }
                else
                {
                    agent.SetDestination(hit.point);
                    isChasingEnemy = false;
                    enemyTarget = null;
                }
                
            }
        }

    }

    private void HandleCombat()
    {
        if (isChasingEnemy && enemyTarget != null)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemyTarget.position);
            if (distanceToEnemy <= attackRange)
            {
                agent.SetDestination(transform.position);
                AttackEnemy();
            }
            else
            {
                agent.SetDestination(enemyTarget.position);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemyTarget = other.gameObject.transform;
            enemyHealth = other.gameObject.GetComponent<IHealth>();
            agent.SetDestination(enemyTarget.position);
            AttackEnemy();
            isChasingEnemy = true;
        }
    }

    private void AttackEnemy()
    {
        if(isAttacking) return; // Prevents the function from executing if already attacking
        StartCoroutine(AttackCooldown());
    }

    private IEnumerator AttackCooldown()
    {
        isAttacking = true;
        anim.SetBool("isAttacking", true);
        yield return new WaitForSeconds(attackDelay);
        PlayRandomAttackSound();
        enemyHealth.TakeDamage(attackDamage);
        anim.SetBool("isAttacking", false);
        isAttacking = false;
        isChasingEnemy = true;
    }
    
    private void PlayRandomAttackSound()
    {
        int randomIndex = Random.Range(0, attackSources.Length);
        attackSources[randomIndex].Play();
    }
}