using UnityEngine;
using System.Collections.Generic;

public class ProceduralSpawn : MonoBehaviour {
    
    public static Enemy GetEnemy (List<Enemy> enemies, float totalWeight) {
        float _randNum = Random.Range(0, totalWeight);
        Enemy _selectedEnemy = null;

        foreach (Enemy enemy in enemies) {
            if (_randNum < enemy.Weight) {
                _selectedEnemy = enemy;
                break;
            }
            _randNum = _randNum - enemy.Weight;
        }
        return _selectedEnemy;
    }

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

            Debug.Log($"1: {_result["Test1"]}, 2: {_result["Test2"]}, 3: {_result["Test3"]}, 4: {_result["Test4"]}");
        }
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