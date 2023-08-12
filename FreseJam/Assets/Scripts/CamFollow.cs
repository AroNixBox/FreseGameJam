using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CamFollow : MonoBehaviour
{
    [SerializeField] private Transform followPosition;
    [SerializeField] private Transform player;
    [SerializeField] private float followDistance = 2.0f; 
    [SerializeField] private float followSpeed = 2.0f;
    [SerializeField] private Animator anim;
    [SerializeField] private float rightOffset = 1.0f;

    private Vector3 lastPosition;
    private float movementThreshold = 0.5f; 

    private void Start()
    {
        lastPosition = transform.position;
    }

    private void Update()
    {
        Vector3 targetPosition = followPosition.position + followPosition.right * rightOffset;


        float distanceToTarget = Vector3.Distance(transform.position, targetPosition);

        if (distanceToTarget >= followDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }

        float speed = (transform.position - lastPosition).magnitude / Time.deltaTime;


        if (speed > movementThreshold)
        {
            anim.SetBool("isFollowing", true);
        }
        else
        {
            anim.SetBool("isFollowing", false);
        }

        // Look at player without changing the up vector (this prevents unwanted tilt)
        Vector3 directionToPlayer = player.position - transform.position;
        directionToPlayer.y = 0; // remove vertical component
        if (directionToPlayer.sqrMagnitude > 0.001f) // Check to avoid zero vector
        {
            transform.rotation = Quaternion.LookRotation(directionToPlayer);
        }

        lastPosition = transform.position;
    }
}