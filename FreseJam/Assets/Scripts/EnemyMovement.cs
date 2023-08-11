using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    public enum MovementState
    {
        Swimming,
        Walking
    }
    public MovementState currentState = MovementState.Swimming;
    public float swimSpeed = 3f;
    public float walkSpeed = 5f;
    private NavMeshAgent agent;
    [SerializeField] private Transform Objective;
    private bool hasReachedBeach;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = swimSpeed;
    }

    void Update()
    {
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
            Debug.Log(walkSpeed);
        }
    }
}