using UnityEngine;

namespace FpvDroneSimulator.Common.Extensions
{
    public static class Vector3Extensions
    {
        public static bool EqualsApproximately(this Vector3 value, Vector3 other, float approximation) =>
            System.Math.Abs(value.x - other.x) < approximation &&
            System.Math.Abs(value.y - other.y) < approximation &&
            System.Math.Abs(value.z - other.z) < approximation;
        
        public static Vector3 NormalizeAngles(this Vector3 eulerRotation)
        {
            return new Vector3(
                eulerRotation.x.NormalizeAngle(),
                eulerRotation.y.NormalizeAngle(),
                eulerRotation.z.NormalizeAngle()
            );
        }
    }
}