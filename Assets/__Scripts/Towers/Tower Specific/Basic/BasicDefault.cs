using UnityEngine;
using System.Collections.Generic;

public class BasicDefault : MonoBehaviour {

    public GameObject _target;
    private GameObject _top;
    
	void Start () {
        _top = transform.FindChild("Top").gameObject;
	}
	
	void Update () {
        if (_target != null) {
            Vector3 _targetPoint = _target.transform.position - _top.transform.position;
            Quaternion _rotation = Quaternion.Slerp(_top.transform.rotation, Quaternion.LookRotation(_targetPoint), 10 * Time.deltaTime);
            _top.transform.rotation = _rotation;
            float y = _top.transform.eulerAngles.y;
            _top.transform.eulerAngles = new Vector3(0, y, 0);
        }
    }
}
