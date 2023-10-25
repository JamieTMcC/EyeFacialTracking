using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace Oculus.Movement.Effects {
    public class ReadOvrExpressions : MonoBehaviour
    {

        protected OVRFaceExpressions _ovrFaceExpressions;

        public MacroFacialExpressionDetector _facialExpressionDetector;

        public TMP_Text text;
        

        // Start is called before the first frame update
        void Start()
        {
            text.text = "";
            InvokeRepeating("Cycle", 0.0f, 0.5f);
        }

        void Cycle()
        {
            text.text = "";
            _ovrFaceExpressions = _facialExpressionDetector.GetOVRExpression();
            if (!_ovrFaceExpressions.FaceTrackingEnabled ||
                !_ovrFaceExpressions.ValidExpressions)
            {
                return;
            }

            OVRFaceExpressions.FaceExpression[] expression = {
                OVRFaceExpressions.FaceExpression.BrowLowererL,
                OVRFaceExpressions.FaceExpression.BrowLowererR};

            foreach(OVRFaceExpressions.FaceExpression ex 
                in expression)
            {
                var RoundedValue = System.Math.Round(_ovrFaceExpressions[ex], 2);
                text.text += ex.ToString() + ": " + RoundedValue.ToString() + "\n";
            }

        }
    }
}