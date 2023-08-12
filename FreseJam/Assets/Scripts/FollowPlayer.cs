using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float followDistance = 2.0f; 
    [SerializeField] private float followSpeed = 2.0f; 

    private void Update()
    {
        if (Vector3.Distance(transform.position, player.position) >= followDistance)
        {
            // Bewegen Sie das Pet sanft in Richtung des Spielers.
            transform.position = Vector3.MoveTowards(transform.position, player.position, followSpeed * Time.deltaTime);
        }
        transform.LookAt(player);
    }
}
