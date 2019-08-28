// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Linq;
using UnityEngine;

namespace Microsoft.MixedReality.Toolkit.Input
{
    /// <summary>
    /// Add a NearInteractionTouchable to your scene and configure a touchable surface
    /// in order to get PointerDown and PointerUp events whenever a PokePointer touches this surface.
    /// </summary>
    public class NearInteractionTouchable : ColliderNearInteractionTouchable, INearInteractionTouchable
    {
        /// <summary>
        /// Local space object center
        /// </summary>
        [SerializeField]
        protected Vector3 localCenter = Vector3.zero;
        public Vector3 LocalCenter { get => localCenter; set { localCenter = value; } }

        /// <summary>
        /// Local space forward direction
        /// </summary>
        [SerializeField]
        protected Vector2 bounds = Vector2.zero;
        public Vector2 Bounds { get => bounds; set { bounds = value; } }

        protected override void OnValidate()
        {
            if (Application.isPlaying)
            {   // Don't validate during play mode
                return;
            }

            base.OnValidate();

            bounds.x = Mathf.Max(bounds.x, 0);
            bounds.y = Mathf.Max(bounds.y, 0);
        }

        /// <inheritdoc />
        public override float DistanceToTouchable(Vector3 samplePoint, out Vector3 normal)
        {
            // The Normal is the backward direction.
            normal = transform.InverseTransformDirection(Vector3.back);

            Vector3 localPoint = transform.InverseTransformPoint(samplePoint) - localCenter;

            // touchables currently can only be touched within the bounds of the rectangle.
            // We return infinity to ensure that any point outside the bounds does not get touched.
            if (localPoint.x < -bounds.x / 2 ||
                localPoint.x > bounds.x / 2 ||
                localPoint.y < -bounds.y / 2 ||
                localPoint.y > bounds.y / 2)
            {
                return float.PositiveInfinity;
            }

            // Scale back to 3D space
            var worldPoint = transform.TransformSize(localPoint);

            return Math.Abs(worldPoint.z);
        }
    }
}