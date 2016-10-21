using UnityEngine;
using System.Collections.Generic;
using CurveExtended;

public class ProceduralSpawn : MonoBehaviour {

    WeightDictionary _enemies = new WeightDictionary();

    void Start () {

    }

    void Update () {
        
    }
}

[System.Serializable]
public struct WeightDictionary {
    public Transform EnemyTansform;
    public float EnemyWeight;
}