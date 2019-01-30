using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RoamingAI : MonoBehaviour {

    NavMeshAgent _agent;
	GameObject _destination;
    bool _hasDestination = false;

	void Start () {
		_agent = GetComponent<NavMeshAgent> ();
        _destination = NPCManager.Instance.getDestination(this);
        moveToDestination();
	}

    void Update()
    {
        if(_hasDestination) {
            if(Vector3.Distance(transform.position, _destination.transform.position) <= 1f) {
                reachedDestination();
            }
        }
    }
	
    /// <summary>
    /// Allows the NPC Manager to send this agent a destination if it was not previously available.
    /// </summary>
	public void SetPatrolPoint (GameObject destination) {
        _destination = destination;
        moveToDestination();
	}

    /// <summary>
    /// Set the nav mesh destionation.
    /// </summary>
    void moveToDestination () {
        if(_destination) {
            _hasDestination = true;
            _agent.SetDestination(_destination.transform.position);
        }
    }

    /// <summary>
    /// Post destination arrival operation.
    /// </summary>
    void reachedDestination () {
        _hasDestination = false;
        NPCManager.Instance.returnDestination(_destination);
        _destination = NPCManager.Instance.getDestination(this);
        moveToDestination();
    }
}
