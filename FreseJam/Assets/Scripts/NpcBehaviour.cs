using UnityEngine;


public class NpcBehaviour : MonoBehaviour
{
    private Transform player;
    [SerializeField] private Transform diePoisition;
    [SerializeField] private GameObject dieParticles;

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
        Instantiate(dieParticles, diePoisition.position, diePoisition.rotation);
        GameManager.Instance.InstantiatePeopleDieSound();
    }
}
