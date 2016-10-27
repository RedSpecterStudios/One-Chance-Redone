using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProceduralSpawn : MonoBehaviour {

    public GameObject mobOne;
    public GameObject mobTwo;
    public GameObject mobThree;
    public GameObject mobFour;

    private float _totalWeight = 0f;
    private Dictionary<GameObject, float> enemies = new Dictionary<GameObject, float>();

    void Start () {
        enemies.Add(mobOne, 15);
        enemies.Add(mobTwo, 20);
        enemies.Add(mobThree, 35);
        enemies.Add(mobFour, 30);
        
        foreach (KeyValuePair<GameObject, float> enemy in enemies) {
            _totalWeight += enemy.Value;
        }

        for (int i = 0; i < 100; i++) {
            Debug.Log(GetEnemy(enemies, _totalWeight));
        }
    }

    public static GameObject GetEnemy (Dictionary<GameObject, float> enemies, float totalWeight) {
        float _randNum = Random.Range(0, totalWeight);
        GameObject _selectedEnemy = null;

        foreach (KeyValuePair<GameObject, float> enemy in enemies) {
            if (_randNum <= enemy.Value) {
                _selectedEnemy = enemy.Key;
                break;
            }
            _randNum = _randNum - enemy.Value;
        }
        return _selectedEnemy;
    }
}