using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(TestProjectile))]
public class TestEditor : Editor {
    public override void OnInspectorGUI() {
        TestProjectile Test = (TestProjectile)target;
        base.OnInspectorGUI();
        if (GUILayout.Button("Launch")) {
            Test.Test();
        }
    }
}