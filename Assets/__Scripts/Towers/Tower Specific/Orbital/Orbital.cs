using UnityEngine;

public class Orbital : MonoBehaviour {

    public GameObject top;

    private Animation _spin;
    
	void Start () {
        _spin = GetComponent<Animation>();
        _spin["OrbitalSpin"].speed = 0.125f;
	}
	
	void Update () {
	
	}
}
