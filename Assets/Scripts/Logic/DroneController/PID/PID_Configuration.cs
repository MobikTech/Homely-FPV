using UnityEngine;

namespace FpvDroneSimulator.Logic.DroneController.PID
{
    [CreateAssetMenu(fileName = "PID_", menuName = "Configuration/PID", order = 1)]
    public class PID_Configuration : ScriptableObject
    {
        public float pGain = 1f;
        public float iGain = 1f;
        public float dGain = 1f;
    }
}