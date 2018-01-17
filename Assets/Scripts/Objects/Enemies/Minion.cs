using UnityEngine;
using UnityEngine.AI;

namespace Objects.Enemies {
    public class Minion : MonoBehaviour {
    
        public Transform Goal;

        private void Start () {
            // Initializes the NavMeshAgent and sets the goal
            var agent = GetComponentInChildren<NavMeshAgent>();
            agent.destination = Goal.position;
        }
    }
}