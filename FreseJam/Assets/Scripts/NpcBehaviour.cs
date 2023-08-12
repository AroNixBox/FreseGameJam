using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class NpcBehaviour : MonoBehaviour
{
    private Transform player;
    [SerializeField] private Transform diePoisition;
    [SerializeField] private GameObject dieParticles;
    [SerializeField] private AudioSource[] dieSounds;

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
        //int randomIndex = Random.Range(0, dieSounds.Length);
        //dieSounds[randomIndex].Play();
    }
}
