using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies {
    public class WeightedSpawning : MonoBehaviour {

        public float SpawnTime = 6;
        public Transform SpawnPoint;
        public SubDict[] LevelMobs;

        private float _totalWeight;
        private GameObject _goal;

        void Start () {
            // Sets goal
            _goal = GameObject.Find("Core 02");
            // Prelim adding of total weights of all mobs for current level
            foreach (var enemy in LevelMobs) {
                _totalWeight += enemy.Weight;
            }
            // Starts the spawn timer
            StartCoroutine(SpawnTimer(SpawnTime));
        }
        // Spawns enemy and sets it's goal
        void SpawnEnemy () {
            var enemy = Instantiate(GetEnemy(LevelMobs, _totalWeight), SpawnPoint.position, Quaternion.identity);
            enemy.GetComponent<Minion>().Goal = _goal.transform;
        }
        // Timer loop for spawning
        IEnumerator SpawnTimer (float timeInSeconds) {
            SpawnEnemy();
            yield return new WaitForSeconds(timeInSeconds);
            StartCoroutine(SpawnTimer(SpawnTime));
        }
        // Determines the next mob to be selected for spawning using randoms and math
        static GameObject GetEnemy (IEnumerable<SubDict> enemies, float totalWeight) {
            var randNum = Random.Range(0, totalWeight);
            GameObject selectedEnemy = null;

            foreach (var enemy in enemies) {
                if (randNum <= enemy.Weight) {
                    selectedEnemy = enemy.Mob;
                    break;
                }
                randNum = randNum - enemy.Weight;
            }
            return selectedEnemy;
        }
    }

// Creates a Serialized "Dictionary", mainly for Inspector use
    [System.Serializable]
    public abstract class SubDict {
        public readonly GameObject Mob;
        public readonly float Weight;

        protected SubDict (GameObject mob, float weight) {
            Mob = mob;
            Weight = weight;
        }
    }
}