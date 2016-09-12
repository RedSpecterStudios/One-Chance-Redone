using UnityEngine;

public class PlaceObject : MonoBehaviour {

    public Camera tpCamera;

    private bool _placing = false;
    private Ray _ray;
    private RaycastHit _hit;

	void Start () {
	
	}

    void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            _placing = !_placing;
        }
    }

    void Place () {
        while (_placing) {
            _ray=tpCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(_ray, out _hit, Mathf.Infinity)) {

            }
        }
    }
}
