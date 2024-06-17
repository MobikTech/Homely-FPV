using System;

namespace FpvDroneSimulator.Logic.DroneController.PID
{
    [Serializable]
    public struct PIDConfigurationInfo
    {
        public String name;
        public PID_Configuration config;
    }
}