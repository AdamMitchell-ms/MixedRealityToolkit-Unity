﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Microsoft.MixedReality.Toolkit.Examples.Demos
{
    ///<summary>
    /// This class exists to route <see cref="Microsoft.MixedReality.Toolkit.UI.PressableButton"/> events through to <see cref="Interactable"/> and/or <see cref="UnityEngine.UI.Button"/>.
    /// The result is being able to have physical touch call Interactable.OnPointerClicked.
    ///</summary>
    public class PhysicalPressEventRouter : MonoBehaviour
    {
        [Tooltip("Interactable to which the press events are being routed. Defaults to the object of the component.")]
        public Interactable routingTarget;

        [Tooltip("Unity UI Button to invoke on press of PressableButton")]
        public Button uiButton;

        /// Enum specifying which button event causes a Click to be raised.
        public enum PhysicalPressEventBehavior
        {
            EventOnClickCompletion = 0,
            EventOnPress,
            EventOnTouch
        }
        public PhysicalPressEventBehavior InteractableOnClick = PhysicalPressEventBehavior.EventOnClickCompletion;

        private void Awake()
        {
            if (routingTarget == null)
            {
                routingTarget = GetComponent<Interactable>();
            }

            if (uiButton == null)
            {
                uiButton = GetComponent<Button>();
            }
        }

        public void OnHandPressTouched()
        {
            if (routingTarget != null)
            {
                routingTarget.SetPhysicalTouch(true);
                if (InteractableOnClick == PhysicalPressEventBehavior.EventOnTouch)
                {
                    routingTarget.SetPress(true);
                    routingTarget.TriggerOnClick();
                    routingTarget.SetPress(false);
                }
            }

            if (uiButton != null && InteractableOnClick == PhysicalPressEventBehavior.EventOnTouch)
            {
                uiButton.onClick.Invoke();
            }
        }

        public void OnHandPressUntouched()
        {
            if (routingTarget != null)
            {
                routingTarget.SetPhysicalTouch(false);
                if (InteractableOnClick == PhysicalPressEventBehavior.EventOnTouch)
                {
                    routingTarget.SetPress(true);
                }
            }
        }

        public void OnHandPressTriggered()
        {
            if (routingTarget != null)
            {
                routingTarget.SetPhysicalTouch(true);
                routingTarget.SetPress(true);
                if (InteractableOnClick == PhysicalPressEventBehavior.EventOnPress)
                {
                    routingTarget.TriggerOnClick();
                }
            }

            if (uiButton != null && InteractableOnClick == PhysicalPressEventBehavior.EventOnPress)
            {
                uiButton.onClick.Invoke();
            }
        }

        public void OnHandPressCompleted()
        {
            if (routingTarget != null)
            {
                routingTarget.SetPhysicalTouch(true);
                routingTarget.SetPress(true);
                if (InteractableOnClick == PhysicalPressEventBehavior.EventOnClickCompletion)
                {
                    routingTarget.TriggerOnClick();
                }
                routingTarget.SetPress(false);
                routingTarget.SetPhysicalTouch(false);
            }

            if (uiButton != null && InteractableOnClick == PhysicalPressEventBehavior.EventOnClickCompletion)
            {
                uiButton.onClick.Invoke();
            }
        }
    }
}