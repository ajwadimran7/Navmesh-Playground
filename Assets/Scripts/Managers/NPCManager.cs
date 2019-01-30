using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour {

    public static NPCManager Instance;
    public List<GameObject> patrolPoints = new List<GameObject> ();
    List<RoamingAI> _agentsQueue = new List<RoamingAI> ();

    void Awake () {
        Instance = this;
    }

    /// <summary>
    /// Sends a patrol point to agent. If all patrol points are taken then it will add the agent to the queue
    /// and wait for a patrol point to be free.
    /// </summary>
    public GameObject getDestination (RoamingAI agent) {
        if(patrolPoints.Count > 0) {
            GameObject tempDestination = patrolPoints[0];
            patrolPoints.RemoveAt(0);
            return tempDestination;
        } else {
            _agentsQueue.Add(agent);
            return null;
        }
    }

    /// <summary>
    /// Return the patrol point back to the list after agent has reached it.
    /// </summary>
    public void returnDestination (GameObject destination) {
        if(_agentsQueue.Count > 0) {
            _agentsQueue[0].SetPatrolPoint (destination);
            _agentsQueue.RemoveAt(0);
        } else {
            patrolPoints.Add(destination);
        }
    }
}