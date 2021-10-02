using Saving;

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using Random = UnityEngine.Random;

[RequireComponent(typeof(NavMeshAgent))]
public class Npc : MonoBehaviour
{
    private NavMeshAgent agent;
    
    [SerializeField] private List<WayPoint> waypoints = new List<WayPoint>();
    
    [Header("-- Animation Settings --")]
    [SerializeField, Tooltip("This agent's animator")] private Animator animator;
    [Header("-- Movement Settings --")]
    [SerializeField, Tooltip("how long to stand still after reaching the destination")] private float standStillTimeLength = 1;
    [SerializeField, Tooltip("How far away from the destination will the AI stop")] private float targetDistanceToWaypoint = 5;
    [SerializeField, Tooltip("how fast the agent will walk")] private float walkSpeed = 5;
    [SerializeField, Tooltip("how fast the agent will run")] private float runSpeed = 10;

    [Header("-- Appearance Settings --")]
    [SerializeField] private AppearanceManager appearanceManager;
    [SerializeField] private AppearanceData npcAppearance;
    
    //how long the agent will stand still for
    private float standStillTimer = 0;
    //whether the agent has arrived yet
    private bool hasArrived = false;
    //whether or not the agent is running
    private bool isRunning = false;

    //will give us a random waypoint in the array as a variable every time i access it
    private WayPoint RandomPoint => waypoints[Random.Range(0, waypoints.Count)];

    private void OnValidate()
    {
        if(appearanceManager)
        {
            appearanceManager.ApplyAppearenceData(npcAppearance);
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();

        // Tell the agent to move ot a random position in the scen waypoints
        agent.stoppingDistance = targetDistanceToWaypoint;
    }
    
    /// <summary>
    /// This will update which target the ai should be navigating towards and also the animation based on what the ai is doing
    /// </summary>
    private void UpdateMovement()
    {
        if(waypoints.Count > 0)
        {
            // has the agent reached its position
            if (hasArrived || (!agent.pathPending && agent.remainingDistance < targetDistanceToWaypoint))// has the agent reached it's position
            {
                //if the agent has stood still for standstilltimelength
                if(standStillTimer > standStillTimeLength)
                {
                    isRunning = false;
                    animator.SetInteger("Movement", 1);
                    if(Random.Range(0, 2) == 1)
                    {
                        Debug.Log("shouldBeRunning");
                        animator.SetInteger("Movement", 2);
                        isRunning = true;
                    }
                    agent.speed = isRunning
                        ? runSpeed
                        : walkSpeed;
                    standStillTimer = 0;
                    // Tell the agent to move ot a random position in the scen waypoints
                    agent.SetDestination(RandomPoint.Position);
                    hasArrived = false;
                    return;
                }
                //set the animation to the idle animation and increase the timer until it is greater than standstilltimelength
                hasArrived = true;
                animator.SetInteger("Movement", 0);
                standStillTimer += Time.deltaTime;
            }
            else
            {
                //make sure the agent is using a moving animation
                animator.SetInteger("Movement", 1);
            }
        }
    }
    
    
    // Update is called once per frame
    void Update()
    {
        UpdateMovement();
    }
}