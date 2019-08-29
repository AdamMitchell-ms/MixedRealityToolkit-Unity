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
        private Lazy<RectTransform> rectTransform;

        public static IReadOnlyList<NearInteractionTouchableUnityUI> Instances => instances;

        public Vector3 LocalCenter => Vector3.zero;

        public Vector2 Bounds => rectTransform.Value.rect.size;

        private static readonly List<NearInteractionTouchableUnityUI> instances = new List<NearInteractionTouchableUnityUI>();

        public NearInteractionTouchableUnityUI()
        {
            rectTransform = new Lazy<RectTransform>(GetComponent<RectTransform>);
        }

        /// <inheritdoc />
        public override float DistanceToTouchable(Vector3 samplePoint, out Vector3 normal)
        {
            // The Normal is the backward direction.
            normal = transform.TransformDirection(Vector3.back);

            Vector3 localPoint = transform.InverseTransformPoint(samplePoint);

            // touchables currently can only be touched within the bounds of the rectangle.
            // We return infinity to ensure that any point outside the bounds does not get touched.
            if (!rectTransform.Value.rect.Contains(localPoint))
            {
                return float.PositiveInfinity;
            }

            // Scale back to 3D space
            var worldPoint = transform.TransformSize(localPoint);

            return Math.Abs(worldPoint.z);
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