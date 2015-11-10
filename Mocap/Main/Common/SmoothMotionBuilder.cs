using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Kinect;

namespace Main
{
    /// <summary>
    /// Конструктор структуры параметров сглаживания движений
    /// </summary>
    public static class SmoothMotionBuilder
    {
        private static float _correction = 0.3f;

        //Gets or sets the jitter radius value.
        private static float _jitterRadius = 1.0f;

        //Gets or sets the maximum deviation radius value.
        private static float _maxDeviationRadius = 1.0f;

        //Gets or sets the prediction value.
        private static float _prediction = 1.0f;

        //Gets or sets the smoothing value.
        private static float _smoothing = 0.7f;

        public static TransformSmoothParameters GetSmoothParameters()
        {
            return new TransformSmoothParameters()
                       {
                           Correction = _correction,
                           JitterRadius = _jitterRadius,
                           MaxDeviationRadius = _maxDeviationRadius,
                           Prediction = _prediction,
                           Smoothing = _smoothing
                       };
        }

        public static void SetSmoothParameters(TransformSmoothParameters value)
        {
            _correction = value.Correction;
            _jitterRadius = value.JitterRadius;
            _maxDeviationRadius = value.MaxDeviationRadius;
            _prediction = value.Prediction;
            _smoothing = value.Smoothing;
        }

        public static void SetHardSmoothParameters()
        {
            _correction = 0.3f;
            _jitterRadius = 1.0f;
            _maxDeviationRadius = 1.0f;
            _prediction = 1.0f;
            _smoothing = 0.7f;
        }

        public static void SetLightSmoothParameters()
        {
            _correction = 0.1f;
            _jitterRadius = 0.1f;
            _maxDeviationRadius = 0.1f;
            _prediction = 0.5f;
            _smoothing = 0.5f;
        }

    }
}
