using UnityEngine;

public class PlaceObject : MonoBehaviour {

    public Camera tpCamera;

    public bool _placing = false;
    private float? _pedX;
    private float? _pedZ;
    private GameObject _pedestal;
    private Ray _ray;
    private RaycastHit _hit;

    void Start () {

    }

    void Update () {
        // Toggles _placing every time the player hits "1" above the qwerty line
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            _placing = !_placing;
        }

        // Calls Place method
        Place();
    }

    void Place () {
        // If _placing equals true...
        if (_placing) {
            // Set _ray to the ray between the main camera and where the mouse pointer is on the screen
            _ray = tpCamera.ScreenPointToRay(Input.mousePosition);
            // If the raycast hits anything in the loaded world...
            if (Physics.Raycast(_ray, out _hit, Mathf.Infinity)) {
                // If the hit object has the tag of "Pedestal"...
                if (_hit.collider.tag == "Pedestal") {
                    // Sets shortcut values and outputs the X and Z co-ords to the debug log
                    _pedestal = _hit.collider.gameObject;
                    _pedX = _pedestal.transform.position.x;
                    _pedZ = _pedestal.transform.position.z;
                    Debug.Log($"X: {_pedX}, Z: {_pedZ}");
                } else {
                    // Else sets the shorcuts to null
                    _pedestal = null;
                    _pedX = null;
                    _pedZ = null;
                }
            }
        }
    }
}
