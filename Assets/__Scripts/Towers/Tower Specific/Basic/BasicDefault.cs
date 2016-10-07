using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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

    private List<GameObject> _enemies;
    
	void Start () {
        StartCoroutine(FireTimer(_fireRate));

        _bulletShooter = GetComponent<BulletShooter>();
        _top = transform.FindChild("Top").gameObject;

        _enemies = new List<GameObject>();
	}
	
	void Update () {
        FindTarget();

        if (_target != null) {
            _dirAB = (_target.transform.position - _top.transform.position).normalized;
            _dotProd = Vector3.Dot(_dirAB, _top.transform.forward);

            _dist = Vector3.Distance(transform.position, _target.transform.position);
            if(_target != null && _dist < _range) {
                Vector3 _targetPoint = _target.transform.position - _top.transform.position;
                Quaternion _rotation = Quaternion.Slerp(_top.transform.rotation, Quaternion.LookRotation(_targetPoint), 10 * Time.fixedDeltaTime);
                _top.transform.rotation = _rotation;
                float y = _top.transform.eulerAngles.y;
                _top.transform.eulerAngles = new Vector3(0, y, 0);
            }
        }
    }

    void FindTarget () {
        List<Collider> _hitColliders = Physics.OverlapCapsule(transform.position - new Vector3(0, 25, 0), transform.position + new Vector3(0, 35, 0), 20).ToList();
        foreach (Collider target in _hitColliders) {

            for (int i = 0; i < _enemies.Count; i++) {
                if (!_hitColliders.Contains(_enemies[i].GetComponent<Collider>())) {
                    _enemies.Remove(_enemies[i]);
                }
            }

            Debug.Log(ArrayOutput(_enemies));

            if(target.tag == "Enemy") {
                if (!_enemies.Contains(target.gameObject)) {
                    _enemies.Add(target.gameObject);
                }
                _target = target.gameObject;
                _bulletShooter.target = target.gameObject;
            }
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

    string ArrayOutput (List<GameObject> _array) {
        string _output = "";

        foreach (GameObject item in _array) {
            _output = _output + item.ToString();
        }

        return _output;
    }

    void OnDrawGizmos () {
        //DebugExtension.DrawCapsule(transform.position - new Vector3(0, 25, 0), transform.position + new Vector3(0, 35, 0), 20);
    }
}
