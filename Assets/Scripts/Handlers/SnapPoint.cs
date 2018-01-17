using System.Collections.Generic;
using UnityEngine;

namespace Handlers {
    public class SnapPoint : MonoBehaviour {

        private int _towersBefore;

        public Transform Towers;

        public static int TowersCount;
    
        public static Dictionary<GameObject, GameObject> SnapPoints;
        public static Dictionary<GameObject, GameObject> TowerPoints;

        // Creates modular array of all available snap points, to be used by the PlaceObject script
        void Start () {
            SnapPoints = new Dictionary<GameObject, GameObject>();
            TowerPoints = new Dictionary<GameObject, GameObject>();

            foreach (var point in GameObject.FindGameObjectsWithTag("Walkway")) {
                SnapPoints.Add(point, null);
            }
            foreach (var point in GameObject.FindGameObjectsWithTag("Pedestal")) {
                SnapPoints.Add(point, null);
            }
            foreach (var point in GameObject.FindGameObjectsWithTag("Tower")) {
                TowerPoints.Add(point, null);
                TowersCount++;
                _towersBefore++;
            }
        }

        void Update() {
            if (TowersCount != _towersBefore) {
                TowerPoints.Clear();
                foreach (var t in Towers.GetComponentsInChildren<Transform>()) {
                    TowerPoints.Add(t.gameObject, null);
                    _towersBefore++;
                }
            }
        }
    }
}
