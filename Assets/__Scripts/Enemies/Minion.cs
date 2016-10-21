using UnityEngine;
using System.Collections;

public class Minion : MonoBehaviour {

    public Transform goal;
    
	void Start () {
        // Initializes the NavMeshAgent and sets the goal
        NavMeshAgent _agent = GetComponentInChildren<NavMeshAgent>();
        _agent.destination = goal.position;
	}
}