using UnityEngine;


public class NpcBehaviour : MonoBehaviour
{
    private Transform player;

    private void Start()
    {
        player = GameManager.Instance.PlayersLocation();
    }

    private void Update()
    {
        Vector3 directionToPlayer = player.position - transform.position;
        directionToPlayer.y = 0; // remove vertical component
        if (directionToPlayer.sqrMagnitude > 0.001f) // Check to avoid zero vector
        {
            transform.rotation = Quaternion.LookRotation(directionToPlayer);
        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.SpawnBloodParticleOnDeadCrewMember(transform);
        GameManager.Instance.InstantiatePeopleDieSound();
    }
}
