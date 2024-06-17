using UnityEngine;

namespace FpvDroneSimulator.Common.Math
{
    public static class BezierCurves
    {
        public static Vector3 CalculateQuadraticBezier(float t, Vector3 p0, Vector3 p1, Vector3 p2)
        {
            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;

            return (uu * p0) + (2 * u * t * p1) + (tt * p2);
        }

        public static Vector3 CalculateCubicBezier(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
        {
            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;
            float uuu = uu * u;
            float ttt = tt * t;
        
            return (uuu * p0) + (3 * uu * t * p1) + (3 * u * tt * p2) + ttt * p3;
        }
    }
}