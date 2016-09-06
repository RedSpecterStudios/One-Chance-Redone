using UnityEngine;
using System.Collections;

public class TopSpin : MonoBehaviour {

    public GameObject top;
	
	void Update () {
        top.transform.Rotate(Vector3.up, Time.deltaTime*50);
	}
}
