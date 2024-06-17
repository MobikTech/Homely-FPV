using FpvDroneSimulator.Common.Extensions;
using UnityEngine;

namespace FpvDroneSimulator.Logic.DroneController
{
    public class Gyroscope : MonoBehaviour
    {
        public Quaternion Rotation => transform.rotation;
        public Vector3 EulerRotation => transform.rotation.eulerAngles.NormalizeAngles();

        public float Pitch => transform.localRotation.eulerAngles.x.NormalizeAngle();
        public float Roll => transform.localRotation.eulerAngles.z.NormalizeAngle();
        
        
        public float AngleOfAttack => Vector3.SignedAngle(transform.up, Vector3.up, transform.forward);
        public bool IsHorizontal => Mathf.Abs(transform.up.y - 1) < 0.0001f;
    }
}
