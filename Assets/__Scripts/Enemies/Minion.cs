using UnityEngine;
using System.Collections;

public class Minion : MonoBehaviour {
    
	void Start () {
	
	}
	
	void Update () {
        transform.Translate(Vector3.forward * Time.deltaTime * 7.5f);
        // https://docs.unity3d.com/Manual/nav-CreateNavMeshAgent.html
    }
}