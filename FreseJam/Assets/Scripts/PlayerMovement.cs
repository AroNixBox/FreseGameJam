using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMovement : MonoBehaviour
{
    private NavMeshAgent _agent;
    [SerializeField]  Camera _mainCamera;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MovePlayer();
        }
    }

    void MovePlayer()
    {
        RaycastHit hit;

        if (Physics.Raycast(_mainCamera.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
        {
            _agent.SetDestination(hit.point);
        }
    }
}
