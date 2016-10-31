using UnityEngine;
using System.Collections;

public class WeightedSpawning : MonoBehaviour {

    public Transform spawnPoint;
    public SubDict[] levelMobs;

    private float _spawnTime = 3;
    private float _totalWeight = 0f;

    void Start () {

        foreach (SubDict enemy in levelMobs) {
            _totalWeight += enemy.weight;
        }

        StartCoroutine(SpawnTimer(_spawnTime));

        /*for (int i = 0; i < 100; i++) {
            Debug.Log(GetEnemy(levelMobs, _totalWeight));
        }*/
    }

    public void SpawnEnemy () {
        Instantiate(GetEnemy(levelMobs, _totalWeight), spawnPoint, Quaternion.identity);
    }

    IEnumerator SpawnTimer (float _timeInSeconds) {
        SpawnEnemy();
        yield return new WaitForSeconds(_timeInSeconds);
        StartCoroutine(SpawnTimer(_spawnTime));
    }

    public static GameObject GetEnemy (SubDict[] enemies, float totalWeight) {
        float _randNum = Random.Range(0, totalWeight);
        GameObject _selectedEnemy = null;

        foreach (SubDict enemy in enemies) {
            if (_randNum <= enemy.weight) {
                _selectedEnemy = enemy.mob;
                break;
            }
            _randNum = _randNum - enemy.weight;
        }
        return _selectedEnemy;
    }
}

[System.Serializable]
public class SubDict {
    public GameObject mob;
    public float weight;
}