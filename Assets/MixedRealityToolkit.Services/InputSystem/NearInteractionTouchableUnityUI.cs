// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Microsoft.MixedReality.Toolkit.Input
{
    /// <summary>
    /// Use a Unity UI RectTransform as touchable surface.
    /// </summary>
    [RequireComponent(typeof(RectTransform))]
    public class NearInteractionTouchableUnityUI : BaseNearInteractionTouchable, INearInteractionTouchable
    {
        private RectTransform rectTransform;

        public static IReadOnlyList<NearInteractionTouchableUnityUI> Instances => instances;

        public Vector3 Forward => transform.TransformDirection(LocalForward);

        public Vector3 LocalForward => -Vector3.forward;

        public Vector3 LocalUp => Vector3.up;

        public Vector3 LocalRight => -Vector3.right;

        [SerializeField]
        private Vector3 localCenter = Vector3.zero;
        public Vector3 LocalCenter => localCenter;

        public Vector2 Bounds => rectTransform.rect.size;

        private static readonly List<NearInteractionTouchableUnityUI> instances = new List<NearInteractionTouchableUnityUI>();

        protected override void OnValidate()
        {
            base.OnValidate();

            rectTransform = GetComponent<RectTransform>();
        }

        public override float DistanceToTouchable(Vector3 samplePoint, out Vector3 normal)
        {
            normal = Forward;

            Vector3 localPoint = transform.InverseTransformPoint(samplePoint);

            // touchables currently can only be touched within the bounds of the rectangle.
            // We return infinity to ensure that any point outside the bounds does not get touched.
            if (!rectTransform.rect.Contains(localPoint))
            {
                return float.PositiveInfinity;
            }

            var localDistance = localPoint - LocalCenter;

            // Scale back to 3D space
            var worldDistance = transform.TransformSize(localDistance);


            return Math.Abs(worldDistance.z);
        }

        protected void OnEnable()
        {
            instances.Add(this);
        }

        protected void OnDisable()
        {
            instances.Remove(this);
        }
    }
}