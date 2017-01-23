using UnityEngine;

public class Gems : MonoBehaviour {

    public GameObject gemBody;
	
	void FixedUpdate () {
        gemBody.transform.Rotate(new Vector3(0, 0, -1.5f));
	}
}
