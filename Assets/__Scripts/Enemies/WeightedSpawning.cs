using UnityEngine;
using System.Collections;

public class WeightedSpawning : MonoBehaviour {

    public float _spawnTime = 6;
    public Transform spawnPoint;
    public SubDict[] levelMobs;

    private float _totalWeight = 0f;
    private GameObject _goal;

    void Start () {
        // Sets goal
        _goal = GameObject.Find("Core 02");
        // Prelim adding of total weights of all mobs for current level
        foreach (SubDict enemy in levelMobs) {
            _totalWeight += enemy.weight;
        }
        // Starts the spawn timer
        StartCoroutine(SpawnTimer(_spawnTime));
    }
    // Spawns enemy and sets it's goal
    public void SpawnEnemy () {
        GameObject _enemy = (GameObject)Instantiate(GetEnemy(levelMobs, _totalWeight), spawnPoint.position, Quaternion.identity);
        _enemy.GetComponent<Minion>().goal = _goal.transform;
    }
    // Timer loop for spawning
    IEnumerator SpawnTimer (float _timeInSeconds) {
        SpawnEnemy();
        yield return new WaitForSeconds(_timeInSeconds);
        StartCoroutine(SpawnTimer(_spawnTime));
    }
    // Determines the next mob to be selected for spawning using randoms and math
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

// Creates a Serialized "Dictionary", mainly for Inspector use
[System.Serializable]
public class SubDict {
    public GameObject mob;
    public float weight;
}