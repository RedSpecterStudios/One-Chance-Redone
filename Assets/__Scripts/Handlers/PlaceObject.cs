using UnityEngine;
using System.Collections.Generic;

public class PlaceObject : MonoBehaviour {

    public Camera tpCamera;
    public List<GameObject> objectList;

    private List<Transform> _allChildren;

    private bool _placing = false;
    private float? _pedX;
    private float? _pedZ;
    private GameObject _itemInHand;
    private GameObject _pedestal;
    private Material _originalMaterial;
    private Ray _ray;
    private RaycastHit _hit;

    void Start () {
        _allChildren = new List<Transform>();
    }

    void Update () {
        // If the player hits any number button 1-8 above the qwerty line
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) ||
            Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Alpha4) ||
            Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Alpha6) ||
            Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Alpha8)) {
            // If _placing is false...
            if (!_placing) {
                // Toggle the true/false value of _placing
                _placing = !_placing;
            }

            // Gets the number the player pressed, parses it to an int, and sets it as _numKey
            int _numKey = int.Parse(Input.inputString);
            // Determines the object the player wants to spawn and sets _itemInHand as the spawned object
            _itemInHand = (GameObject)Instantiate(objectList[_numKey-1], Vector3.zero, Quaternion.identity);
            // For each child in _itemInHand, if it has a Box Collider, change it's layer to ignore Raycasts
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
                // Move the object being spawned to where the mouse is over
                _itemInHand.transform.position = _hit.point;
                // If the hit object has the tag of "Pedestal"...
                if (_hit.collider.tag == "Pedestal") {
                    // Sets shortcut values
                    _pedestal = _hit.collider.gameObject;
                    _pedX = _pedestal.transform.position.x;
                    _pedZ = _pedestal.transform.position.z;


                    foreach (Transform child in _allChildren) {
                        if (child.GetComponent<MeshRenderer>() != null) {
                            Material mat = child.GetComponent<Renderer>().material;
                            _originalMaterial = mat;
                            mat = _itemInHand.GetComponent<Tower>().ghostMaterial;
                            child.GetComponent<Renderer>().material = mat;
                        }
                    }


                    // Sets the _itemInHand to "snap" to the top-center of the pedestal so it looks like it's on it
                    _itemInHand.transform.position = new Vector3((float)_pedX, CalculateTopPosition(_pedestal), (float)_pedZ);
                } else {
                    // Else sets the shorcuts to null
                    _pedestal = null;
                    _pedX = null;
                    _pedZ = null;
                }
            }
        }
    }

    // Calculates the top-most global y position of any object by finding the center global y position of the object
    // and adding half the height of the object
    float CalculateTopPosition (GameObject _object) {
        float _top = 0;
        
        _top = _pedestal.GetComponent<Renderer>().bounds.center.y + (_pedestal.GetComponent<Renderer>().bounds.size.y/2);

        return _top;
    }
}
