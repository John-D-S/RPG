using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Checkpoint : MonoBehaviour
{
    [SerializeField] private int checkpointNumber;
    private GameObject player;
    
    private void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            ProgressionManager.theProgressionManager.currentCheckpoint = checkpointNumber;
        }
    }
}
