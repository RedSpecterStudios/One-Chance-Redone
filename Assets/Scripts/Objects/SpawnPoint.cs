using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Objects {
	public class SpawnPoint : MonoBehaviour {
		public static List<GameObject> EnemyArray = new List<GameObject>();

		private const string EnemyTag = "Enemy";

		void Start () {
			InvokeRepeating(nameof(ListClosestEnemies), 0, 0.066f);
		}

		void ListClosestEnemies () {
			EnemyArray = GameObject.FindGameObjectsWithTag(EnemyTag)
				.OrderBy(enemy => Vector3.Distance(transform.position, enemy.transform.position))
				.ToList();
		}
	}
}
