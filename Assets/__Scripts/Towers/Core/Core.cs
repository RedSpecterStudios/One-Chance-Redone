using UnityEngine;

public class Core : MonoBehaviour {
    
    void OnTriggerEnter (Collider other) {
        if (other.tag == "Enemy") {
            print("Enemy entered");
            Destroy(other.transform.parent.gameObject, 1.5f);
        }
    }
}
