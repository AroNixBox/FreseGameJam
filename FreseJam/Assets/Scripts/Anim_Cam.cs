using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim_Cam : MonoBehaviour
{
    [SerializeField] private Transform player;

    // Update is called once per frame
    void Update()
    {
        Vector3 directionToPlayer = player.position - transform.position;
        directionToPlayer.y = 0; // remove vertical component
        if (directionToPlayer.sqrMagnitude > 0.001f) // Check to avoid zero vector
        {
            transform.rotation = Quaternion.LookRotation(directionToPlayer);
        } 
    }
}
