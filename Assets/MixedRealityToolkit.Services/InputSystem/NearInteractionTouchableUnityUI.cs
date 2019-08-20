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

        public Vector3 LocalForward => Vector3.forward;

        public Vector3 LocalUp => Vector3.up;

        public Bounds LocalTouchCage
        {
            get
            {
                if (rectTransform == null)
                {
                    rectTransform = GetComponent<RectTransform>();
                    // Start() has not been called yet.  Return reasonable defaults
                    //return new Bounds(Vector3.zero, Vector3.one);
                }
                
                //else
                {
                    var z = Mathf.Max(rectTransform.rect.width, rectTransform.rect.height);
                    return new Bounds(Vector3.zero, new Vector3(rectTransform.rect.width, rectTransform.rect.height, z));
                }
            }
        }

        private static readonly List<NearInteractionTouchableUnityUI> instances = new List<NearInteractionTouchableUnityUI>();

        /// <inheritdoc />
        void Start()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        public override float DistanceToTouchable(Vector3 samplePoint, out Vector3 normal)
        {
            normal = -transform.forward;

            Vector3 localPoint = transform.InverseTransformPoint(samplePoint);

            // touchables currently can only be touched within the bounds of the rectangle.
            // We return infinity to ensure that any point outside the bounds does not get touched.
            if (!rectTransform.rect.Contains(localPoint))
            {
                return float.PositiveInfinity;
            }

            // Scale back to 3D space
            localPoint = transform.TransformSize(localPoint);

            return Math.Abs(localPoint.z);
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