using UnityEngine;
using System.Collections.Generic;

public class SnapPoint : MonoBehaviour {

    private int _towersBefore = 0;

    public Transform _Towers;

    public static int Towers = 0;
    
    public static Dictionary<GameObject, GameObject> SnapPoints;
    public static Dictionary<GameObject, GameObject> TowerPoints;

    // Creates modular array of all available snap points, to be used by the PlaceObject script
    void Start () {
        SnapPoints = new Dictionary<GameObject, GameObject>();
        TowerPoints = new Dictionary<GameObject, GameObject>();

        foreach (GameObject _point in GameObject.FindGameObjectsWithTag("Walkway")) {
            SnapPoints.Add(_point, null);
        }
        foreach (GameObject _point in GameObject.FindGameObjectsWithTag("Pedestal")) {
            SnapPoints.Add(_point, null);
        }


        foreach (Transform t in _Towers.GetComponentsInChildren<Transform>()) {
            TowerPoints.Add(t.gameObject, null);
            _towersBefore++;
            Towers++;
        }
    }

    void Update() {
        if (Towers != _towersBefore) {
            TowerPoints.Clear();
            foreach (Transform t in _Towers.GetComponentsInChildren<Transform>()) {
                TowerPoints.Add(t.gameObject, null);
                _towersBefore++;
            }
        }
    }
}
