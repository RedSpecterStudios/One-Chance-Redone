using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BasicDefault : MonoBehaviour {

    public bool _canFire = true;
    private float _dist;
    private float _dotProd;
    private float _fireRate = 5;
    private float _range = 20f;
    private BulletShooter _bulletShooter;
    private GameObject _target;
    private GameObject _top;
    private Vector3 _dirAB;
    
	void Start () {
        StartCoroutine(FireTimer(_fireRate));

        _bulletShooter = GetComponent<BulletShooter>();
        _top = transform.FindChild("Top").gameObject;
	}
	
	void Update () {
        FindTarget();

        _dirAB = (_target.transform.position - _top.transform.position).normalized;
        _dotProd = Vector3.Dot(_dirAB, _top.transform.forward);

        _dist = Vector3.Distance(transform.position, _target.transform.position);
        if (_target != null && _dist < _range) {
            Vector3 _targetPoint = _target.transform.position - _top.transform.position;
            Quaternion _rotation = Quaternion.Slerp(_top.transform.rotation, Quaternion.LookRotation(_targetPoint), 10 * Time.fixedDeltaTime);
            _top.transform.rotation = _rotation;
            float y = _top.transform.eulerAngles.y;
            _top.transform.eulerAngles = new Vector3(0, y, 0);
        }
    }

    void FindTarget () {
        Collider[] hitColliders = Physics.OverlapSphere(new Vector3(0,0,0), 10);
        foreach (Collider target in hitColliders) {
            Debug.Log(target.gameObject);
            // http://answers.unity3d.com/questions/208192/see-the-size-of-an-overlap-sphere.html
        }
    }

    void Fire () {
        if (_dotProd >= 0.9 && _dist < _range) {
            _bulletShooter.Shoot();
        }
    }

    IEnumerator FireTimer (float fireRate) {
        Fire();
        yield return new WaitForSeconds(fireRate);
        StartCoroutine(FireTimer(_fireRate));
    }
}
