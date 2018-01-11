using UnityEngine;

namespace Gems {
	public class Gems : MonoBehaviour {

		public GameObject GemBody;
	
		void FixedUpdate () {
			GemBody.transform.Rotate(new Vector3(0, 0, -1.5f));
		}
	}
}
