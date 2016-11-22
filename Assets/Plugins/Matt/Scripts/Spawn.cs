using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {
    
    public static CreateSplineProfile NewSpline(GameObject Target, CreateSplineProfile Original, GameObject Source) {
        CreateSplineProfile Spline = Instantiate(Original);
        Spline.name = Original.name;
        Spline.Caster = Source;
        Spline.Victim = Target;
        Spline.Target = Target.transform.position;
        Spline.Source = Source.transform.position;
        Spline.Points[0] = Spline.Source;
        Spline.Points[3] = Spline.Target;
        return Spline;
    }
}
