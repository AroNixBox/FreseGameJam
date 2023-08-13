using System.Collections;
using UnityEngine;

public class SharkSpawner : MonoBehaviour
{
    [SerializeField] private GameObject sharkPrefab;
    [SerializeField] private GameObject orcabossPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float initialSpawnDelay = 10f;
    [SerializeField] private float spawnReductionRate = 0.95f;
    [SerializeField] private float minimumSpawnDelay = 2f; 

    private float currentSpawnDelay;

    private void Start()
    {
        currentSpawnDelay = initialSpawnDelay;
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(currentSpawnDelay);
            SpawnShark();
            currentSpawnDelay *= spawnReductionRate;
            if (currentSpawnDelay < minimumSpawnDelay)
                currentSpawnDelay = minimumSpawnDelay;
            
            
        }
    }

    public void SpawnOrca()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform chosenSpawnPoint = spawnPoints[randomIndex];
        Instantiate(orcabossPrefab, chosenSpawnPoint.position, chosenSpawnPoint.rotation);
    }

    private void SpawnShark()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform chosenSpawnPoint = spawnPoints[randomIndex];
        Instantiate(sharkPrefab, chosenSpawnPoint.position, chosenSpawnPoint.rotation);
    }
}