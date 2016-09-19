using UnityEditor;
using UnityEngine;
using System;

public class TowerStats : ScriptableObject {
    [Serializable]
    public class TowerStatValues {
        public float minRange;
        public float maxRange;
        public float health;
        public float fireRate;
        public float damage;
    }

    public TowerStatValues basicTower;
    public TowerStatValues gatlingTower;
    public TowerStatValues mine;
    public TowerStatValues missleTower;
    public TowerStatValues mortorTower;
    public TowerStatValues orbitalTower;
    public TowerStatValues railCannonTower;
    public TowerStatValues sniperTower;

    [MenuItem("Assets/Create New Asset")]
    public static void CreateAsset () {
        AssetDatabase.CreateAsset(CreateInstance<TowerStats>(), "Assets/__Scripts/Towers/TowerStats.asset");
    }

}
