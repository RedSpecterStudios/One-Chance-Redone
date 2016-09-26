using UnityEngine;
using System.Collections;

public class BulletShooter : MonoBehaviour {
    
    public float TravelTime;
    public CreateSplineProfile Spline;
    public GameObject Source;
    public GameObject Target;

    public void Shoot() {
        GameObject ObjectToMove = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        ObjectToMove.AddComponent<ParticleMover>();
        ObjectToMove.GetComponent<ParticleMover>().Source = Source;
        ObjectToMove.GetComponent<ParticleMover>().Target = Target;
        ObjectToMove.GetComponent<ParticleMover>().Spline = Spline;
        ObjectToMove.GetComponent<ParticleMover>().Launch(TravelTime);
    }
}
