using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(BulletShooter))]
public class TestEditor : Editor {
    public override void OnInspectorGUI() {
        BulletShooter Test = (BulletShooter)target;
        base.OnInspectorGUI();
        if (GUILayout.Button("Launch")) {
            Test.Shoot();
        }
    }
}