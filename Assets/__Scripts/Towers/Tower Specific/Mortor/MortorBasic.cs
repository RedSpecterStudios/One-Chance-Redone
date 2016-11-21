using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MortorBasic : MonoBehaviour {

    public GameObject top;

    private float _dist;
    private float _dotProd;
    private float _fireRate = 2f;
    private float _rangeMax = 45f;
    private float _rangeMin = 15f;
    private int _mode = 1;
    private BulletShooter _bulletShooter;
    private GameObject _lastEntered;
    private GameObject _target;
    private Vector3 _dirAB;

    private List<GameObject> _enemies = new List<GameObject>();

    void Start () {
        StartCoroutine(FireTimer(_fireRate));
        
        _bulletShooter = GetComponent<BulletShooter>();
    }

    void Update () {
        // Finds the target
        FindTarget();
        // Follows the target, when their is one and it's within the range of the tower
        if (_target != null) {
            _dirAB = (_target.transform.position - top.transform.position).normalized;
            _dotProd = Vector3.Dot(_dirAB, top.transform.forward);

            _dist = Vector3.Distance(transform.position, _target.transform.position);
            Vector3 _targetPoint = _target.transform.position - top.transform.position;
            Quaternion _rotation = Quaternion.Lerp(top.transform.rotation, Quaternion.LookRotation(_targetPoint), 25 * Time.fixedDeltaTime);
            top.transform.rotation = _rotation;
            float y = top.transform.eulerAngles.y;
            top.transform.eulerAngles = new Vector3(0, y, 0);
        }
    }

    // Honestly, I don't even know how some of this works but it does so... ¯\_(ツ)_/¯
    void FindTarget () {
        // Sets the range sphere
        List<Collider> _hitColliders = Physics.OverlapCapsule(transform.position - new Vector3(0, 35, 0), transform.position + new Vector3(0, 75, 0), _rangeMax).ToList();
        List<Collider> _tooClose = Physics.OverlapCapsule(transform.position - new Vector3(0, 35, 0), transform.position + new Vector3(0, 75, 0), _rangeMin).ToList();
        foreach (Collider target in _hitColliders) {
            // Removes the target from the list of enemies, and nulls the target when they leave the range
            for (int i = 0; i < _enemies.Count; i++) {
                if (!_hitColliders.Contains(_enemies[i].GetComponent<Collider>())) {
                    if (_enemies[i] = _target) {
                        _target = null;
                        _bulletShooter.target = _target;
                    }
                    _enemies.Remove(_enemies[i]);
                }
            }
            // If the object caught in the range is an enemy, add it to the list of enemies
            if (target.tag == "Enemy") {
                if (!_enemies.Contains(target.gameObject)) {
                    _enemies.Add(target.gameObject);
                    _lastEntered = target.gameObject;
                }
                // Finds the target based on the mode of the tower
                switch (_mode) {
                    #region Closest Target
                    case 1:
                        // If their is more than one enemy in the list, look for the closest and target it, else target the only target
                        if (_enemies.Count > 1) {
                            GameObject _closest;
                            _closest = _enemies[0];
                            foreach (GameObject cTar in _enemies) {
                                if (Vector3.Distance(cTar.transform.position, transform.position) <
                                    Vector3.Distance(_closest.transform.position, transform.position)) {
                                    _closest = cTar;
                                }
                            }
                            _target = _closest;
                        } else {
                            if (!_tooClose.Contains(_enemies[0].GetComponent<Collider>())) {
                                _target = _enemies[0];
                            } else {
                                break;
                            }
                        }
                        break;
                    #endregion
                    #region Farthest Target
                    case 2:
                        // If their is more than one enemy in the list, look for the farthest and target it, else target the only target
                        if (_enemies.Count > 1) {
                            GameObject _farthest;
                            _farthest = _enemies[0];
                            foreach (GameObject fTar in _enemies) {
                                if (Vector3.Distance(fTar.transform.position, transform.position) >
                                    Vector3.Distance(_farthest.transform.position, transform.position)) {
                                    _farthest = fTar;
                                }
                            }
                            _target = _farthest;
                        } else {
                            _target = _enemies[0];
                        }
                        break;
                    #endregion
                    #region First Target
                    case 3:
                        // Targets the first enemy in the array, aka the target closest to the end.
                        if (_enemies.Count > 0) {
                            _target = _enemies[0];
                        }
                        break;
                    #endregion
                    #region Last Target
                    case 4:
                        // Target the enemy that last entered the range. Why would any heathan even think about using this?
                        _target = _lastEntered;
                        break;
                    #endregion
                    default:
                        // If for whatever reason the mode is invalid, target nothing
                        _target = null;
                        break;
                }

                if (_target.transform.parent.FindChild("Center") != null) {
                    _target = _target.transform.parent.FindChild("Center").gameObject;
                } else {
                    Debug.LogWarning($"No \"Center\" child in \"{_target}\"");
                }

                _bulletShooter.target = _target;
            }
        }
    }

    void Fire () {
        // If the shot is realistic looking, and the target is in the range, fire at it
        if (_dotProd >= 0.7 && _dist < _rangeMax && _dist > _rangeMin) {
            _bulletShooter.Shoot();
        }
    }

    IEnumerator FireTimer (float _timeToWait) {
        Fire();
        yield return new WaitForSeconds(_timeToWait);
        StartCoroutine(FireTimer(_fireRate));
    }

    void OnDrawGizmos () {
        DebugExtension.DrawCapsule(transform.position - new Vector3(0, 75, 0), transform.position + new Vector3(0, 75, 0), Color.red, _rangeMax);
        DebugExtension.DrawCapsule(transform.position - new Vector3(0, 75, 0), transform.position + new Vector3(0, 75, 0), Color.blue, _rangeMin);
    }
}
