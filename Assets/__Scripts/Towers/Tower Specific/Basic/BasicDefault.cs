using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BasicDefault : MonoBehaviour {

    private bool _canFire;
    private float _fireRate;
    private float _range = 20f;
    public GameObject _target;
    private GameObject _top;
    
	void Start () {
        StartCoroutine(fireTimer(_fireRate));

        _top = transform.FindChild("Top").gameObject;
	}
	
	void Update () {
        float dist = Vector3.Distance(transform.position, _target.transform.position);
        if (_target != null && dist < _range) {
            Vector3 _targetPoint = _target.transform.position - _top.transform.position;
            Quaternion _rotation = Quaternion.Slerp(_top.transform.rotation, Quaternion.LookRotation(_targetPoint), 10 * Time.deltaTime);
            _top.transform.rotation = _rotation;
            float y = _top.transform.eulerAngles.y;
            _top.transform.eulerAngles = new Vector3(0, y, 0);
        } else {
            
        }
    }

    void Fire () {

    }

    IEnumerator fireTimer (float fireRate) {
        Fire();
        yield return new WaitForSeconds(fireRate);
    }
}
