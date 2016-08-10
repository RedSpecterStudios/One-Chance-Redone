using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(SplineManager))]
public class SplineProfileEditor : Editor
{
    // Distance along a line: (Origin - Distance * (Origin - (Vector + Origin)))
    public bool StartEditor;

    private void OnSceneGUI()
    {
        SplineManager Spline = (SplineManager)target;

        if (StartEditor)
        {
            Spline.Spline.OscillationRangeH = Spline.OscillationRangeH;
            Spline.Spline.OscillationSpeedH = Spline.OscillationSpeedH;
            Spline.Spline.OscillationRangeV = Spline.OscillationRangeV;
            Spline.Spline.OscillationSpeedV = Spline.OscillationSpeedV;

            Spline.Spline.OrbitDistance = Spline.OrbitRange;
            Spline.Spline.OrbitSpeed = Spline.OrbitSpeed;

            Spline.Spline.Points[0] = Spline.Source.transform.position;
            Spline.Spline.Points[3] = Spline.Target.transform.position;

            if (Spline.AutoRandom != true)
            {
                Spline.Spline.Points[1] = (Spline.Spline.Points[0] - (Spline.Spline.ControlPointLength1 * Spline.Spline.SplineScale) * (Spline.Spline.Points[0] - (Spline.Spline.ControlPointVector1 + Spline.Spline.Points[0])));
                Spline.Spline.Points[2] = (Spline.Spline.Points[3] + (Spline.Spline.ControlPointLength2 * Spline.Spline.SplineScale) * (Spline.Spline.Points[3] - (Spline.Spline.ControlPointVector2 + Spline.Spline.Points[3])));
                Spline.SplineScale = Spline.CurrentLength / Spline.OriginalLength;
                Spline.Spline.SplineScale = Spline.SplineScale;
                Spline.Spline.SplineLength = Vector3.Distance(Spline.Spline.Caster.transform.position, Spline.Spline.Victim.transform.position);
            }

            Spline.Spline.RandomD = Spline.RandomD;
            Spline.Spline.RandomH = Spline.RandomH;
            Spline.Spline.RandomV = Spline.RandomV;

            Spline.Spline.Randomize = Spline.AutoRandom;
            Spline.Spline.Reflection = Spline.RandomizerReflection;

            Vector3 Source = Spline.Source.transform.position;
            Vector3 Target = Spline.Target.transform.position;

            Vector3 Center = SplineMaker2.GetPoint(Spline.Spline.Points[0], Spline.Spline.Points[1], Spline.Spline.Points[2], Spline.Spline.Points[3], Spline.T);
            //Vector3 Tangent = SplineMaker2.GetTangent(Spline.Spline.Points[0], Spline.Spline.Points[1], Spline.Spline.Points[2], Spline.Spline.Points[3], Spline.T);

            Vector3 TangentVector = SplineMaker2.GetDirection(Spline.T, Spline.Spline, Spline.Source.transform);
            Vector3 TangentPoint = Center - Spline.DirectionDistance * (Center - (TangentVector + Center));

            Vector3 BiNormal = SplineMaker2.GetBiNormal(TangentVector);
            Vector3 BiNormalPoint = Center - Spline.DirectionDistance * (Center - (BiNormal + Center));

            Vector3 Normal = SplineMaker2.GetNormal(TangentVector, BiNormal);
            Vector3 NormalPoint = Center - Spline.DirectionDistance * (Center - (Normal + Center));

            Spline.CurrentLength = Vector3.Distance(Spline.Source.transform.position, Spline.Target.transform.position);

            if (Spline.LockScale == false)
            {
                Spline.Spline.OriginalLength = Spline.OriginalLength;
                Spline.OriginalLength = Vector3.Distance(Spline.Source.transform.position, Spline.Target.transform.position);
            }

            // create the handles to edit the points

            if (Spline.EditorHandles == true)
            {
                if (Spline.LockOrigins != true)
                {
                    Quaternion handleRotation0 = Tools.pivotRotation == PivotRotation.Local ? Quaternion.identity : Quaternion.identity; ;
                    Handles.DoPositionHandle(Spline.Spline.Points[0], handleRotation0);
                    EditorGUI.BeginChangeCheck();
                    Vector3 p0 = Handles.DoPositionHandle(Source, handleRotation0);
                    if (EditorGUI.EndChangeCheck())
                    {
                        Spline.Spline.Points[0] = p0;
                        Spline.Spline.SplineLength = Vector3.Distance(Spline.Spline.Points[0], Spline.Spline.Points[3]);
                        Spline.Spline.LenghtVector = Vector3.Normalize(Spline.Spline.Points[3] - Spline.Spline.Points[0]);
                    }

                    Quaternion handleRotation3 = Tools.pivotRotation == PivotRotation.Local ? Quaternion.identity : Quaternion.identity; ;
                    Handles.DoPositionHandle(Spline.Spline.Points[3], handleRotation3);
                    EditorGUI.BeginChangeCheck();
                    Vector3 p3 = Handles.DoPositionHandle(Target, handleRotation3);
                    if (EditorGUI.EndChangeCheck())
                    {
                        Spline.Spline.Points[3] = p3;
                        Spline.Spline.SplineLength = Vector3.Distance(Spline.Spline.Points[0], Spline.Spline.Points[3]);
                        Spline.Spline.LenghtVector = Vector3.Normalize(Spline.Spline.Points[3] - Spline.Spline.Points[0]);
                    }
                }

                if (Spline.ShowControlPoints)
                {
                    Quaternion handleRotation1 = Tools.pivotRotation == PivotRotation.Local ? Quaternion.identity : Quaternion.identity; ;
                    Handles.DoPositionHandle(Spline.Spline.Points[1], handleRotation1);
                    EditorGUI.BeginChangeCheck();
                    Vector3 p1 = Handles.DoPositionHandle(Spline.Spline.Points[1], handleRotation1);
                    if (EditorGUI.EndChangeCheck())
                    {
                        Spline.Spline.Points[1] = p1;
                        Spline.Spline.ControlPointLength1 = Vector3.Distance(Spline.Spline.Points[0], Spline.Spline.Points[1]);
                        Spline.Spline.ControlPointVector1 = Vector3.Normalize(Spline.Spline.Points[1] - Spline.Spline.Points[0]);
                    }


                    Quaternion handleRotation2 = Tools.pivotRotation == PivotRotation.Local ? Quaternion.identity : Quaternion.identity; ;
                    Handles.DoPositionHandle(Spline.Spline.Points[2], handleRotation2);
                    EditorGUI.BeginChangeCheck();
                    Vector3 p2 = Handles.DoPositionHandle(Spline.Spline.Points[2], handleRotation2);
                    if (EditorGUI.EndChangeCheck())
                    {
                        Spline.Spline.Points[2] = p2;
                        Spline.Spline.ControlPointLength2 = Vector3.Distance(Spline.Spline.Points[2], Spline.Spline.Points[3]);
                        Spline.Spline.ControlPointVector2 = Vector3.Normalize(Spline.Spline.Points[3] - Spline.Spline.Points[2]);
                    }
                }


            }


            // draw stuff here
            //control points
            Handles.color = Spline.ControlPointColor;
            Handles.SphereCap(0, Spline.Spline.Points[0], Quaternion.identity, Spline.ControlPointSize * 1.5f);
            Handles.Label(Spline.Spline.Points[0] + Vector3.up * Spline.LabelHeight, "Start");
            if (Spline.ShowControlPoints)
            {
                Handles.SphereCap(1, Spline.Spline.Points[1], Quaternion.identity, Spline.ControlPointSize * 1.5f);
                Handles.Label(Spline.Spline.Points[1] + Vector3.up * Spline.LabelHeight, "Control Point 1");
                Handles.SphereCap(2, Spline.Spline.Points[2], Quaternion.identity, Spline.ControlPointSize * 1.5f);
                Handles.Label(Spline.Spline.Points[2] + Vector3.up * Spline.LabelHeight, "Control Point 2");
                Handles.DrawLine(Spline.Spline.Points[0], Spline.Spline.Points[1]);
                Handles.DrawLine(Spline.Spline.Points[2], Spline.Spline.Points[3]);
            }


            Handles.SphereCap(3, Spline.Spline.Points[3], Quaternion.identity, Spline.ControlPointSize * 1.5f);
            Handles.Label(Spline.Spline.Points[3] + Vector3.up * Spline.LabelHeight, "End");

            Handles.DrawBezier(Spline.Spline.Points[0], Spline.Spline.Points[3], Spline.Spline.Points[1], Spline.Spline.Points[2], Spline.ControlPointColor, null, Spline.SplineLineWidth);

            if (Spline.OscillateHorizontal)
            {
                Spline.Spline.OscillateH = true;
                Handles.SphereCap(5, SplineMaker2.HOscialltionPoint(Center, BiNormal, Spline.OscillationRangeH, Spline.OscillationSpeedH, Spline.T), Quaternion.identity, Spline.ControlPointSize * 1.5f);
                Vector3 OscP1 = Center - Spline.OscillationRangeH / 2 * (Center - (BiNormal + Center));

                Vector3 OscP2 = Center + Spline.OscillationRangeH / 2 * (Center - (BiNormal + Center));
                Handles.DrawLine(OscP1, OscP2);

            }
            else
            {
                Spline.Spline.OscillateH = false;
            }

            if (Spline.OscillateVertical)
            {
                Spline.Spline.OscillateV = true;
                Handles.SphereCap(5, SplineMaker2.VOscialltionPoint(Center, Normal, Spline.OscillationRangeH, Spline.OscillationSpeedH, Spline.T), Quaternion.identity, Spline.ControlPointSize * 1.5f);
                Vector3 OscP1 = Center - Spline.OscillationRangeV / 2 * (Center - (Normal + Center));

                Vector3 OscP2 = Center + Spline.OscillationRangeV / 2 * (Center - (Normal + Center));
                Handles.DrawLine(OscP1, OscP2);

            }
            else
            {
                Spline.Spline.OscillateV = false;
            }

            if (Spline.Orbit)
            {
                Spline.Spline.Orbit = true;
                Vector3 OrbitPoint = SplineMaker2.GetOrbitPoint(Center, TangentVector, BiNormal, Spline.Spline, Spline.OrbitRange);
                Handles.SphereCap(5, OrbitPoint, Quaternion.identity, Spline.ControlPointSize * 1.5f);
                Handles.DrawLine(OrbitPoint, Center);

            }
            else
            {
                Spline.Spline.Orbit = false;
            }

            if (Spline.FollowSpline)
            {
                Spline.Spline.FollowSpline = true;
                Handles.SphereCap(4, Center, Quaternion.identity, Spline.ControlPointSize * 1.5f);
            }
            else
            {
                Spline.Spline.FollowSpline = false;
            }

            if (Spline.ShowDirection)
            {
                Handles.ConeCap(1, TangentPoint, Quaternion.FromToRotation(Vector3.forward, TangentVector), Spline.ControlPointSize * 1.5f);
                Handles.DrawLine(Center, TangentPoint);
                Handles.Label(TangentPoint + Vector3.up * Spline.LabelHeight, "Tangent");

                Handles.ConeCap(2, BiNormalPoint, Quaternion.FromToRotation(Vector3.forward, BiNormal), Spline.ControlPointSize * 1.5f);
                Handles.DrawLine(Center, BiNormalPoint);
                Handles.Label(BiNormalPoint + Vector3.up * Spline.LabelHeight, "BiNormal");


                Handles.ConeCap(2, NormalPoint, Quaternion.FromToRotation(Vector3.forward, Normal), Spline.ControlPointSize * 1.5f);
                Handles.DrawLine(Center, NormalPoint);
                Handles.Label(NormalPoint + Vector3.up * Spline.LabelHeight, "Normal");

                Vector3 lineStart = Spline.Source.transform.position;
                Handles.color = Color.green;
                Handles.DrawLine(lineStart, lineStart + SplineMaker2.GetDirection(0f, Spline.Spline, Spline.Source.transform));

                for (int i = 1; i <= Spline.lineSteps; i++)
                {
                    Vector3 lineEnd = SplineMaker2.GetPoint(Spline.Spline.Points[0], Spline.Spline.Points[1], Spline.Spline.Points[2], Spline.Spline.Points[3], i / (float)Spline.lineSteps);
                    Handles.color = Color.white;
                    Handles.DrawLine(lineStart, lineEnd);
                    Handles.color = Spline.ControlPointColor;

                    Handles.DrawLine(lineEnd, lineEnd + SplineMaker2.GetDirection(i / (float)Spline.lineSteps, Spline.Spline, Spline.Source.transform) * Spline.DirectionDistance);
                    lineStart = lineEnd;
                }
            }







            if (GUI.changed)
            {
                EditorUtility.SetDirty(Spline.Spline);
                EditorUtility.SetDirty(Spline);
            }
        }

    }


    public override void OnInspectorGUI()
    {
        SplineManager Spline = (SplineManager)target;

        Vector3 Target = new Vector3(0, 0, 0);
        Vector3 Source = new Vector3(0, 0, 0);

        if (Spline.Source != null)
        {
            Source = Spline.Source.transform.position;
        }

        if (Spline.Target != null)
        {
            Target = Spline.Target.transform.position;
        }


        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Start Editor"))
        {
            StartEditor = true;
        }
        if (GUILayout.Button("Stop Editor"))
        {
            StartEditor = false;
        }
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Reset"))
        {
            Spline.Spline.Points[0] = Source;
            Spline.Spline.Points[1] = new Vector3(-5, 0, 0);
            Spline.Spline.Points[2] = new Vector3(0, 0, 0);
            Spline.Spline.Points[3] = Target;
        }

        if (Spline.AutoRandom)
        {
            if (GUILayout.Button("Randomize Spline"))
            {
                SplineMaker2.RandomControlPoints(Spline.Source, Spline.Target, Spline.Spline);
            }
        }

        base.OnInspectorGUI();
        if (GUI.changed)
        {
            EditorUtility.SetDirty(Spline.Spline);
            EditorUtility.SetDirty(Spline);
        }
    }


}
