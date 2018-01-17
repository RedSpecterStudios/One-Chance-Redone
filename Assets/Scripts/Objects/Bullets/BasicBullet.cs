using UnityEngine;

namespace Objects.Bullets {
	public class BasicBullet : MonoBehaviour {
		private float _damage;
		private Transform _target;

		private const float Speed = 60;

		public void InheritedProperties (Transform target, float damage) {
			_target = target;
			_damage = damage;
		}

		private void Update () {
			transform.LookAt(_target);

			GetComponent<Rigidbody>().velocity = transform.forward * Speed;
		}

		private void OnCollisionEnter (Collision other) {
			if (other.transform.CompareTag("Enemy")) {
				// Give enemy damage
				Destroy(this);
			}
		}
	}
}
