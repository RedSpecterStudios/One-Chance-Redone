using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GatlingDefault : MonoBehaviour {
    
    public GameObject top;
    public GameObject barrel;
    
    private float _dist;
    private float _dotProd;
    private float _fireRate = .1f;
    private float _range = 20f;
    private float _revLerp = 0;
    private short _mode = 1;
    private Animation _rev;
    private GameObject _lastEntered;
    private GameObject _target;
    private Vector3 _dirAB;

    private List<GameObject> _enemies = new List<GameObject>();

    void Start () {
        StartCoroutine(FireTimer(_fireRate));

        _rev = GetComponent<Animation>();
        // Makes sure the gattling barrel isn't spinning when the game starts
        _rev["GatlingSpin"].speed = 0;
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

    void FixedUpdate() {
        // Revs up the barrel until at max speed, if a target is present
        // Lets the barrel spin itself down until stopping, if their is no longer a target
        if (_target != null) {
            if (_revLerp < 1) {
                _revLerp += Time.fixedDeltaTime / 2;
            } else {
                _revLerp = 1;
            }
        } else {
            if (_revLerp > 0) {
                _revLerp -= Time.fixedDeltaTime / 3;
            } else {
                _revLerp = 0;
            }
        }
        _rev["GatlingSpin"].speed = _revLerp;
    }

    void FindTarget () {
        // Sets the range sphere
        List<Collider> _hitColliders = Physics.OverlapCapsule(transform.position - new Vector3(0, 25, 0), transform.position + new Vector3(0, 35, 0), 20).ToList();
        foreach (Collider target in _hitColliders) {
            // Removes the target from the list of enemies, and nulls the target when they leave the range
            for (int i = 0; i < _enemies.Count; i++) {
                if (!_hitColliders.Contains(_enemies[i].GetComponent<Collider>())) {
                    if (_enemies[i] = _target) {
                        _target = null;
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
                            _target = _enemies[0];
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

                if (_target.transform.parent.Find("Center") != null) {
                    _target = _target.transform.parent.Find("Center").gameObject;
                } else {
                    Debug.LogWarning($"No \"Center\" child in \"{_target}\"");
                }
            }
        }
    }

    void Fire () {
        // If the shot is realistic looking, and the target is in the range, fire at it
        if (_dotProd >= 0.7 && _dist < _range) {
        }
    }

    IEnumerator FireTimer (float _timeToWait) {
        Fire();
        yield return new WaitForSeconds(_timeToWait);
        StartCoroutine(FireTimer(_fireRate));
    }
}
