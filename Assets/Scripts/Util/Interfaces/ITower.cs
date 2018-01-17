using System.Collections;

namespace Util.Interfaces {
	public interface ITower {
		void Start ();
		void Update ();
		void FindTarget ();
		void Fire ();
		IEnumerator FireTimer ();
	}
}
