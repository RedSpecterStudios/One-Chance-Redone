using UnityEngine;
using System.Collections.Generic;

public class PlaceObject : MonoBehaviour {

    public Camera tpCamera;
    public List<GameObject> objectList;

    private bool _placing = false;
    private float? _pedX;
    private float? _pedZ;
    public GameObject _itemInHand;
    private GameObject _pedestal;
    private Ray _ray;
    private RaycastHit _hit;

    void Start () {

    }

    void Update () {
        // Toggles _placing every time the player hits a button from 1-8 above the qwerty line
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) ||
            Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Alpha4) ||
            Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Alpha6) ||
            Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Alpha8)) {
            if (!_placing) {
                _placing = !_placing;
            }

            int _numKey = int.Parse(Input.inputString);
            _itemInHand = (GameObject)Instantiate(objectList[_numKey-1], Vector3.zero, Quaternion.identity);
            foreach (Transform child in _itemInHand.transform) {
                if (child.GetComponent<BoxCollider>() != null) {
                    child.gameObject.layer = 2;
                }
            }

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
                _itemInHand.transform.position = _hit.point;
                // If the hit object has the tag of "Pedestal"...
                if (_hit.collider.tag == "Pedestal") {
                    // Sets shortcut values and outputs the X and Z co-ords to the debug log
                    _pedestal = _hit.collider.gameObject;
                    _pedX = _pedestal.transform.position.x;
                    _pedZ = _pedestal.transform.position.z;
                    _itemInHand.transform.position = new Vector3((float)_pedX, _itemInHand.transform.position.y, (float)_pedZ);
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
