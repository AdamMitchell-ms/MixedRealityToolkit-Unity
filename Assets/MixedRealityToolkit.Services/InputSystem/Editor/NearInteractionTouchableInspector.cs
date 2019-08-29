// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using UnityEngine;

namespace Microsoft.MixedReality.Toolkit.Input
{
    [UnityEditor.CustomEditor(typeof(NearInteractionTouchableUnityUI), true)]
    public class NearInteractionTouchableUnityUIInspector : NearInteractionTouchableInspectorBase
    { }

    [UnityEditor.CustomEditor(typeof(NearInteractionTouchable), true)]
    public class NearInteractionTouchableInspector : NearInteractionTouchableInspectorBase
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var t = (NearInteractionTouchable)target;
            BoxCollider bc = t.GetComponent<BoxCollider>();
            RectTransform rt = t.GetComponent<RectTransform>();
            if (bc != null)
            {
                // project size to local coordinate system
                Vector2 adjustedSize = new Vector2(
                            Math.Abs(Vector3.Dot(bc.size, Vector3.right)),
                            Math.Abs(Vector3.Dot(bc.size, Vector3.up)));

                // Resize helper
                if (adjustedSize != t.Bounds)
                {
                    UnityEditor.EditorGUILayout.HelpBox("Bounds do not match the BoxCollider size", UnityEditor.MessageType.Warning);
                    if (GUILayout.Button("Fix Bounds"))
                    {
                        UnityEditor.Undo.RecordObject(t, "Fix Bounds");
                        t.Bounds = adjustedSize;
                    }
                }
            }
            else if (rt != null)
            {
                // Resize Helper
                if (rt.sizeDelta != t.Bounds)
                {
                    UnityEditor.EditorGUILayout.HelpBox("Bounds do not match the RectTransform size", UnityEditor.MessageType.Warning);
                    if (GUILayout.Button("Fix Bounds"))
                    {
                        UnityEditor.Undo.RecordObject(t, "Fix Bounds");
                        t.Bounds = rt.sizeDelta;
                    }
                }
            }
        }
    }

    public class NearInteractionTouchableInspectorBase : UnityEditor.Editor
    {
        private readonly Color handleColor = Color.white;
        private readonly Color fillColor = new Color(0, 0, 0, 0);

        protected virtual void OnSceneGUI()
        {
            var t = (INearInteractionTouchable)target;

            if (Event.current.type == EventType.Repaint)
            {
                UnityEditor.Handles.color = handleColor;

                Vector3 center = t.transform.TransformPoint(t.LocalCenter);

                float arrowSize = UnityEditor.HandleUtility.GetHandleSize(center) * 0.75f;
                UnityEditor.Handles.ArrowHandleCap(0, center, Quaternion.LookRotation(t.transform.rotation * Vector3.back), arrowSize, EventType.Repaint);

                Vector3 rightDelta = t.transform.localToWorldMatrix.MultiplyVector(Vector3.right * t.Bounds.x / 2);
                Vector3 upDelta = t.transform.localToWorldMatrix.MultiplyVector(Vector3.up * t.Bounds.y / 2);

                Vector3[] points = new Vector3[4];
                points[0] = center + rightDelta + upDelta;
                points[1] = center - rightDelta + upDelta;
                points[2] = center - rightDelta - upDelta;
                points[3] = center + rightDelta - upDelta;

                UnityEditor.Handles.DrawSolidRectangleWithOutline(points, fillColor, handleColor);
            }
        }
    }
}
