using FpvDroneSimulator.Services.SettingsSaveLoader;

namespace FpvDroneSimulator.PersistentData
{
    public class DefaultSettingsProvider : IDefaultDataProvider<SettingsData>
    {
        public SettingsData DefaultData => new()
        {
            Mass = 1,
            MinDragCoefficient = 0.35f,
            MaxDragCoefficient = 1f,
            AngularDrag = 2f,
            MaxTorque = 0.2f,
            MaxThrottle = 6f,
            MaxAngleOfAttack = 60f,
            MaxSpeed = 10f,
            MaxLiftSpeed = 10f,
            MaxAngularSpeed = 30f,
            MaxPropellersRotationSpeed = 10_000f,
        };
    }
}