using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float attackRange = 2.0f;
    [SerializeField] private float attackDamage = 10f;
    [SerializeField] private float attackDelay = 1.0f;
    [SerializeField] private Camera cam;

    private NavMeshAgent agent;
    private Transform enemyTarget;
    private IHealth enemyHealth;
    private bool isAttacking = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        HandleInput();
        HandleCombat();
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 5f);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {
                Debug.Log(hit.collider.gameObject);
                EnemyMovement enemy = hit.collider.GetComponent<EnemyMovement>();
                if (enemy)
                {
                    
                    enemyTarget = hit.collider.transform;
                    enemyHealth = hit.collider.GetComponent<IHealth>();
                    agent.SetDestination(enemyTarget.position);
                    isAttacking = true;
                }
                else
                {
                    agent.SetDestination(hit.point);
                    isAttacking = false;
                    enemyTarget = null;
                }
            }
        }
    }

    private void HandleCombat()
    {
        if (isAttacking && enemyTarget != null)
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

    private void AttackEnemy()
    {
        if (!isAttacking) return;

        enemyHealth.TakeDamage(attackDamage);
        isAttacking = false;

        StartCoroutine(AttackCooldown());
    }

    private System.Collections.IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackDelay);
        isAttacking = true;
    }
}