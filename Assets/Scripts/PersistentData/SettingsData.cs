using FpvDroneSimulator.Services.SettingsSaveLoader;

namespace FpvDroneSimulator.PersistentData
{
    public struct SettingsData : IPersistentData<SettingsData>
    {
        public float Mass;

        // Depends on drone's surface facing velocity direction (angle of attack)
        public float MinDragCoefficient;
        public float MaxDragCoefficient;

        public float AngularDrag;
        
        public float MaxTorque;
        public float MaxThrottle;
        
        // Movement params
        public float MaxAngleOfAttack;

        // Speed params
        public float MaxSpeed;
        public float MaxLiftSpeed;
        public float MaxAngularSpeed;
        
        // Max params of the propellers animation
        public float MaxPropellersRotationSpeed;
    }
}