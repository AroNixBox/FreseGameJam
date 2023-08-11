using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float attackRange = 2.0f;
    [SerializeField] private float attackDamage = 10f;
    [SerializeField] private float attackDelay = 1.0f;

    private NavMeshAgent agent;
    //current target
    private Transform enemyTarget;
    private bool isAttacking = false;

    [SerializeField] private Camera cam;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Debug.Log(hit.collider);
                EnemyMovement enemy = hit.collider.GetComponent<EnemyMovement>();
                if (enemy)
                {
                    enemyTarget = hit.collider.transform;
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

        if (enemyTarget)
        {
            agent.SetDestination(enemyTarget.position);
        }

        if (isAttacking && enemyTarget != null)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemyTarget.position);
            if (distanceToEnemy <= attackRange)
            {
                agent.SetDestination(transform.position);
                AttackEnemy();
            }
        }
    }

    private void AttackEnemy()
    {
        if (!isAttacking) return;

        enemyTarget.GetComponent<IHealth>().TakeDamage(attackDamage);
        isAttacking = false;
        Invoke("ResetAttack", attackDelay);
    }

    private void ResetAttack()
    {
        isAttacking = true;
    }
}
