using System;
using System.Collections;
using System.Linq;
using Objects.Bullets;
using UnityEngine;
using Util;
using Util.Interfaces;

namespace Objects.Towers {
	public class Basic : MonoBehaviour, ITower {

		public float Damage = 10;
		public float FireRate = 5;
		public float Range = 20;
		public GameObject Bullet;
		public Transform FirePoint;
		public TowerMode Mode = TowerMode.First;
		public Transform Top;
		
		private bool _canFire;
		private float _dot;
		private Coroutine _fireTimerCoroutine;
		private Transform _target;

		private const string EnemyTag = "Enemy";
		
		// Links CanFire to _canFire and OnCanFireEvent
		private bool CanFire {
			set {
			    if (_canFire != value) {
			        _canFire = value;

			        OnCanFireEvent?.Invoke(value);
			    }
			}
		}
		
		// Listens to OnCanFireEvent and starts FindTarget loop
		public void Start () {
			OnCanFireEvent += FiringHander;
			
			InvokeRepeating(nameof(FindTarget), 0, 0.066f);
		}

		// Looks at the target
		public void Update () {
			if (_target != null) {
				_dot = Vector3.Dot((_target.position - Top.position).normalized, Top.forward);
				
				var targetPoint = _target.position - Top.position;
				var rotation = Quaternion.Slerp(Top.rotation, Quaternion.LookRotation(targetPoint), 10 * Time.fixedDeltaTime);
				Top.rotation = rotation;
				var y = Top.eulerAngles.y;
				Top.eulerAngles = new Vector3(0, y, 0);
			}
		}

		// Finds target
		public void FindTarget () {
			var enemies = GameObject.FindGameObjectsWithTag(EnemyTag).ToList();

			if (enemies.Count > 0) {
				switch (Mode) {
					// Finds the first target
					case TowerMode.First:
						_target = Core.EnemyArray
							.FirstOrDefault(enemy => Vector3.Distance(transform.position, enemy.transform.position) <= Range)?.transform;
						break;
					// Finds the last target
					case TowerMode.Last:
						_target = SpawnPoint.EnemyArray
							.FirstOrDefault(enemy => Vector3.Distance(transform.position, enemy.transform.position) <= Range)?.transform;
						break;
					// Finds the clostest target to tower
					case TowerMode.Closest:
						_target = enemies
							.OrderBy(enemy => Vector3.Distance(transform.position, enemy.transform.position))
							.FirstOrDefault(enemy => Vector3.Distance(transform.position, enemy.transform.position) <= Range)?.transform;
						break;
					// Finds the farthest target from tower
					case TowerMode.Furthest:
						_target = enemies.OrderByDescending(enemy => Vector3.Distance(transform.position, enemy.transform.position))
							.FirstOrDefault(enemy => Vector3.Distance(transform.position, enemy.transform.position) <= Range)?.transform;
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}

				CanFire = _target != null;
			} else {
				CanFire = false;
			}
		}

		// Fires a shot, if the angle isn't extreme
		public void Fire () {
			if (_dot >= 0.7) {
				var bullet = Instantiate(Bullet, FirePoint.position, Quaternion.identity);
				bullet.GetComponent<BasicBullet>().InheritedProperties(_target, Damage);
			}
		}

		// Reload timer
		public IEnumerator FireTimer () {
			while (true) {
				yield return new WaitForSeconds(FireRate);
				
				Fire();
			}
		}

		// Starts or stops the firing loop, based on canFire
		private void FiringHander (bool canFire) {
			if (canFire) {
				_fireTimerCoroutine = StartCoroutine(FireTimer());
			} else {
				StopCoroutine(_fireTimerCoroutine);
			}
		}

		// Sets up OnCanFireEvent
		public delegate void OnCanFireDelegate (bool value);

		public static event OnCanFireDelegate OnCanFireEvent;
	}
}
