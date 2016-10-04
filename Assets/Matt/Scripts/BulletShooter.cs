using UnityEngine;
using System.Collections;

public class BulletShooter : MonoBehaviour {
    
    public float travelTime;
    public CreateSplineProfile spline;
    public GameObject bulletToMove;
    public GameObject source;
    public GameObject target;

    public void Shoot() {
        GameObject ObjectToMove = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        ObjectToMove.AddComponent<ParticleMover>();
        ObjectToMove.GetComponent<ParticleMover>().Source = source;
        ObjectToMove.GetComponent<ParticleMover>().Target = target;
        ObjectToMove.GetComponent<ParticleMover>().Spline = spline;
        ObjectToMove.GetComponent<ParticleMover>().Launch(travelTime);
    }
}
