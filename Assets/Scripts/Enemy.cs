using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private Transform playerTransform;
    private NavMeshAgent nav;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (playerTransform == null)
        {
            Debug.LogError("Player not found. Make sure the player has the tag 'Player'");
        }

        nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform != null)
        {
            // Update the destination less frequently (e.g., every 0.5 seconds)
            if (Time.frameCount % 30 == 0)
            {
                nav.destination = playerTransform.position;
            }
        }
    }
}