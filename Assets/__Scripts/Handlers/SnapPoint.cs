using UnityEngine;
using System.Collections.Generic;

public class SnapPoint : MonoBehaviour {
    
    public static Dictionary<GameObject, GameObject> snapPoints;

    void Start () {
        snapPoints = new Dictionary<GameObject, GameObject>();

        foreach (GameObject _point in GameObject.FindGameObjectsWithTag("Walkway")) {
            snapPoints.Add(_point, null);
        }
        foreach (GameObject _point in GameObject.FindGameObjectsWithTag("Pedestal")) {
            snapPoints.Add(_point, null);
        }

        TowerStats.CreateAsset();
    }
}
