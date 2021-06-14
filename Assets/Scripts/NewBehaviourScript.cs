using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NewBehaviourScript : MonoBehaviour
{
    public Transform[] waypoint;
    public int currentWaypoint = 0;
    public int roll = 0;
    public NavMeshAgent agent;
    public GameObject AgentLocation;
    public float distance;
    
    void Update()
    {
        agent.SetDestination(waypoint[currentWaypoint].position);

         distance = (AgentLocation.transform.position - waypoint[currentWaypoint].position).magnitude;

        if (distance < 5f)
        {
            roll = Random.Range(0, 3);
            currentWaypoint = roll;
        }
    }
}
