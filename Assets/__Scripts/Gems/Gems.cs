using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Gems : MonoBehaviour {

    public GameObject gemBody;
    
	void Start () {
		
	}
	
	void Update () {
        gemBody.transform.Rotate(new Vector3(0, 0, -1.5f));
	}
}
