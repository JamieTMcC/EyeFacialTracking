// Copyright (c) Meta Platforms, Inc. and affiliates.

using System;
using UnityEngine;
using UnityEngine.Assertions;
using static Oculus.Movement.Effects.MacroFacialExpressionDetector;

namespace Oculus.Movement.Effects
{
    /// <summary>
    /// Reacts to smile detection by modifying the face material on
    /// the Aura asset.
    /// </summary>
    public class AngryEffect : MonoBehaviour
    {
        /// <summary>
        /// Returns the current state of if smile is enabled or disabled.
        /// </summary>
        [SerializeField]
        protected bool _angryEnabled = false;
        /// <summary>
        /// Returns the current state of if smile is enabled or disabled.
        /// </summary>
        public bool AngryEnabled { get => AngryEnabled; set => AngryEnabled = value; }

        /// <summary>
        /// Facial expression detector to query events from.
        /// </summary>
        [SerializeField]
        [Tooltip(SmileEffectTooltips.FacialExpressionDetector)]
        protected MacroFacialExpressionDetector _facialExpressionDetector;

        /// <summary>
        /// Delay until angry gets triggered (seconds).
        /// </summary>
        [SerializeField]
        protected float _angryDelay = 0.2f;

        /// <summary>
        /// State name for smile.
        /// </summary>
        [SerializeField]
        protected string _angryStateName = "Angry";

        /// <summary>
        /// State name for reverse smile (when it "undoes" itself).
        /// </summary>
        [SerializeField]
        protected string _reverseAngryStateName = "ReverseAngry";

        private float _AngryTime = -1.0f;

        private void Awake()
        {
            Assert.IsNotNull(_facialExpressionDetector);

            _facialExpressionDetector.MacroExpressionStateChange +=
                MacroExpressionStateChange;
        }

        private void MacroExpressionStateChange(
            MacroFacialExpressionDetector.MacroExpressionStateChangeEventArgs eventArgs)
        {
            if (!AngryEnabled)
            {
                return;
            }
            if (eventArgs.Expression != MacroExpressionType.Angry)
            {
                return;
            }

            if (eventArgs.State == MacroExpressionState.Active)
            {
                // delay smile
                _AngryTime = Time.time;
            }
            else if (eventArgs.State == MacroExpressionState.Inactive)
            {
                // stop delayed smile if one exists
                _AngryTime = -1.0f;
            }
        }
    }
}
