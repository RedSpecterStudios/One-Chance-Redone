using UnityEngine;
using System.Collections;

public class BulletShooter : MonoBehaviour {
    
    public float travelTime;
    public CreateSplineProfile spline;
    public GameObject bulletToMove;
    public GameObject source;
    public GameObject target;

    public void Shoot() {
        if (bulletToMove == null) {
            bulletToMove = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        }
        bulletToMove.AddComponent<ParticleMover>();
        bulletToMove.GetComponent<ParticleMover>().Source = source;
        bulletToMove.GetComponent<ParticleMover>().Target = target;
        bulletToMove.GetComponent<ParticleMover>().Spline = spline;
        bulletToMove.GetComponent<ParticleMover>().Launch(travelTime);
    }
}
