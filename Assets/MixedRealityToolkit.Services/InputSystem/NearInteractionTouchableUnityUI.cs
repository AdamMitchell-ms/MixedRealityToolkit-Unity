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

        //TODO: protected?
        //TODO: does this need to be settable?
        [SerializeField]
        protected Vector3 localCenter = Vector3.zero;
        public Vector3 LocalCenter { get => localCenter; set { localCenter = value; } }

        //TODO: protected?
        //TODO: settable?
        //TODO: serializable?  Or just derived from RectTransform?

        /// <summary>
        /// Local space forward direction
        /// </summary>
        [SerializeField]
        protected Vector2 bounds = Vector2.zero;
        public Vector2 Bounds { get => bounds; set { bounds = value; } }

        private static readonly List<NearInteractionTouchableUnityUI> instances = new List<NearInteractionTouchableUnityUI>();

        /// <inheritdoc />
        void Start()
        {
            rectTransform = GetComponent<RectTransform>();
            //TODO: Fix up inspector to set these bounds correctly at validate time
            Bounds = rectTransform.rect.size;
            Debug.LogWarning($"Near touchable UI - bounds - {rectTransform.rect.size}");
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