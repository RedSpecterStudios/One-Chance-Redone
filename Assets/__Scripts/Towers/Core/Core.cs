using UnityEngine;

public class Core : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Enemy") {
            Destroy(other, 1.5f);
        } else {
            print(other.tag);
        }
    }
}
