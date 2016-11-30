using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Minion : MonoBehaviour {

    public float distance;
    public Transform goal;

    private void Start () {
        // Initializes the NavMeshAgent and sets the goal
        NavMeshAgent _agent = GetComponentInChildren<NavMeshAgent>();
        _agent.destination = goal.position;
	}

    private void Update () {
        distance = Vector3.Distance(transform.position, goal.position);
    }
}