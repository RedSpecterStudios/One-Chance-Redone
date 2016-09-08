using UnityEngine;

public class PlaceObject : MonoBehaviour {

    private Ray _ray;
    private RaycastHit _hit;

	void Start () {
	
	}

    void Update () {
        if (Physics.Raycast(_ray, out _hit, Mathf.Infinity)) {
            Debug.DrawLine(_ray.origin, _hit.point, Color.red);
        }
    }
}
