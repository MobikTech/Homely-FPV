using UnityEngine;

namespace FpvDroneSimulator.Logic.DroneController
{
    public class HorizonFlightValuesProvider : FlightValuesProviderBase
    {
        public override void ApplyInput(FlightValues flightValues)
        {
            // The input is converted to an angular frequency in X and Z (Pitch, Roll and Yaw).
            // The only difference between Horizon and Manual is that Horizon uses PID to stabilize the angular velocity.
            Vector3 targetAngularVelocity = new Vector3(
                droneInfoHolder.DroneSettings.pitchRollCurve.Evaluate(pitchInput),
                droneInfoHolder.DroneSettings.yawCurve.Evaluate(yawInput),
                droneInfoHolder.DroneSettings.pitchRollCurve.Evaluate(rollInput)
            );
            targetAngularVelocity *= droneInfoHolder.DroneSettings.maxAngularSpeed;

            if (droneInfoHolder.PIDController.enabled)
            {
                Vector3 pitchYawRollCorrection = droneInfoHolder.PIDController.AdjustAngularVelocity(targetAngularVelocity);

                flightValues.Pitch = pitchYawRollCorrection.x;
                flightValues.Yaw = pitchYawRollCorrection.y;
                flightValues.Roll = pitchYawRollCorrection.z;
            }

            // Lift only processed by curves
            flightValues.Lift = droneInfoHolder.DroneSettings.liftCurve.Evaluate(liftInput);
        }
    }
}