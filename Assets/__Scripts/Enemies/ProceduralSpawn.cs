using UnityEngine;
using System.Collections.Generic;

public class ProceduralSpawn : MonoBehaviour {

    private float _1 = 0;
    private float _2 = 0;
    private float _3 = 0;
    private float _4 = 0;
    private float _ran = 0;

    void Start () {
        List<Enemy> enemies = new List<Enemy>();
        enemies.Add(new Enemy("Test1", 10));
        enemies.Add(new Enemy("Test2", 15));
        enemies.Add(new Enemy("Test3", 35));
        enemies.Add(new Enemy("Test4", 30));

        float _totalWeight = 0f;
        foreach (Enemy enemy in enemies) {
            _totalWeight += enemy.Weight;
        }

        for (int i = 0; i < 100; i++) {
            Dictionary<string, float> _result = new Dictionary<string, float>();

            Enemy _selectedEnemy = null;
            for (int j = 0; j < 100; j++) {
                _selectedEnemy = GetEnemy(enemies, _totalWeight);
                if (_selectedEnemy != null) {
                    if (_result.ContainsKey(_selectedEnemy.EnemyTransform)) {
                        _result[_selectedEnemy.EnemyTransform]++; 
                    } else {
                        _result.Add(_selectedEnemy.EnemyTransform, 1);
                    }
                }
            }

            _1 += _result["Test1"];
            _2 += _result["Test2"];
            _3 += _result["Test3"];
            _4 += _result["Test4"];
            _ran++;

            Debug.Log($"Test1: {_result["Test1"]}, Test2: {_result["Test2"]}, Test3: {_result["Test3"]}, Test4: {_result["Test4"]}");
        }
        Debug.Log($"Test1: {_1/_ran}, Test2: {_2/_ran}, Test3: {_3/_ran}, Test4: {_4/_ran}, _ran: {_ran}, Total: {(_1+_2+_3+_4)/_ran}");
    }

    public static Enemy GetEnemy (List<Enemy> enemies, float totalWeight) {
        float _randNum = Random.Range(0, totalWeight);
        Enemy _selectedEnemy = null;

        foreach (Enemy enemy in enemies) {
            if (_randNum <= enemy.Weight) {
                _selectedEnemy = enemy;
                break;
            }
            _randNum = _randNum - enemy.Weight;
        }
        return _selectedEnemy;
    }
}

public class Enemy {
    public string EnemyTransform;
    public float Weight = 0f;

    public Enemy (string t, float w) {
        EnemyTransform = t;
        Weight = w;
    }
}