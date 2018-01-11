using UnityEngine;

public class Core : MonoBehaviour {

    void OnTriggerEnter (Collider other) {
        if (other.CompareTag("Enemy")) {
            Destroy(other.transform.parent.gameObject, 1.5f);
        }
    }
}
