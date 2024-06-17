using FpvDroneSimulator.PersistentData;

namespace FpvDroneSimulator.Logic.DroneController.Extensions
{
    public static class DroneSettingsExtensions
    {
        public static void SetValuesFrom(this DroneSettingsSO droneSettings, SettingsData settingsData)
        {
            droneSettings.mass = settingsData.Mass;
            droneSettings.minDragCoefficient = settingsData.MinDragCoefficient;
            droneSettings.maxDragCoefficient = settingsData.MaxDragCoefficient;
            droneSettings.angularDrag = settingsData.AngularDrag;
            droneSettings.maxTorque = settingsData.MaxTorque;
            droneSettings.maxThrottle = settingsData.MaxThrottle;
            droneSettings.maxAngleOfAttack = settingsData.MaxAngleOfAttack;
            droneSettings.maxSpeed = settingsData.MaxSpeed;
            droneSettings.maxLiftSpeed = settingsData.MaxLiftSpeed;
            droneSettings.maxAngularSpeed = settingsData.MaxAngularSpeed;
            droneSettings.maxRotationSpeed = settingsData.MaxPropellersRotationSpeed;
        }
    }
}