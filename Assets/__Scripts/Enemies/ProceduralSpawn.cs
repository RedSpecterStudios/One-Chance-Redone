using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProceduralSpawn : MonoBehaviour {

    public Transform mobOne;
    public Transform mobTwo;
    public Transform mobThree;
    public Transform mobFour;

    private float _1 = 0;
    private float _2 = 0;
    private float _3 = 0;
    private float _4 = 0;
    private float _ran = 0;

    void Start () {
        Dictionary<Transform, float> enemies = new Dictionary<Transform, float>();
        enemies.Add(mobOne, 15);
        enemies.Add(mobTwo, 20);
        enemies.Add(mobThree, 35);
        enemies.Add(mobFour, 30);

        float _totalWeight = 0f;
        foreach (KeyValuePair<Transform, float> enemy in enemies) {
            _totalWeight += enemy.Value;
        }
        for (int i = 0; i < enemies.Count; i++) {
            _totalWeight += enemies[i];
        }

        for (int i = 0; i < 100; i++) {
            Dictionary<string, float> _result = new Dictionary<string, float>();

            //Enemy _selectedEnemy = null;
            for (int j = 0; j < _totalWeight; j++) {
                //_selectedEnemy = GetEnemy(enemies, _totalWeight);
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
        }
        Debug.Log($"Weight 15: {_1/_ran}, Weight 20: {_2/_ran}, Weight 35: {_3/_ran}, Weight 30: {_4/_ran}, How many times ran: {_ran}, Total: {(_1+_2+_3+_4)/_ran}");
    }

    /*public static Enemy GetEnemy (List<Enemy> enemies, float totalWeight) {
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
    }*/
}