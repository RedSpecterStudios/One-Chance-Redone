using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParticleMover : MonoBehaviour {

    public GameObject Source;
    public GameObject Target;
    public CreateSplineProfile Spline;
    public float T;

    public void Launch(float Time) {
        if(Spline.Randomize) {
            Spline = Spawn.NewSpline(Target, Spline, Source);
            SplineMaker2.RandomControlPoints(Source, Target, Spline);
        } else {
            Spline = Spawn.NewSpline(Target, Spline, Source);
        }

        StartCoroutine(MoveObject(Time));
    }


    public IEnumerator MoveObject(float duration) {
        float startTime = Time.time;

        while (Time.time < startTime + (duration * Spline.SplineScale)) {
            T = Mathf.Lerp(0, 1, (Time.time - startTime) / (duration * Spline.SplineScale));
            SplineMaker2.TraverseSpline(Source, Target, Spline, gameObject, T);
            yield return 0f;
        }

        if (Target.activeInHierarchy) {
            //put your code that triggers damage/explosions or whatever here
        }
        Destroy(Spline);
        Destroy(this.gameObject);
    }
}
