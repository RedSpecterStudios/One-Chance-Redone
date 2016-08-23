﻿using UnityEngine;
using System.Collections;

public class TestProjectile : MonoBehaviour {

    public CreateSplineProfile Spline;
    public GameObject Source;
    public GameObject Target;
    public float TravelTime;

    public void Test()
    {
        GameObject ObjectToMove = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        ObjectToMove.AddComponent<ParticleMover>();
        ObjectToMove.GetComponent<ParticleMover>().Source = Source;
        ObjectToMove.GetComponent<ParticleMover>().Target = Target;
        ObjectToMove.GetComponent<ParticleMover>().Spline = Spline;
        ObjectToMove.GetComponent<ParticleMover>().Launch(TravelTime);
    }
}