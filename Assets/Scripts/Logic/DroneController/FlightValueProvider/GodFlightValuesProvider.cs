using UnityEngine;

namespace FpvDroneSimulator.Logic.DroneController
{
    public class GodFlightValuesProvider : FlightValuesProviderBase
    {
        public override void ApplyInput(FlightValues flightValues)
        {
            Vector3 targetRotation = new Vector3(
                droneInfoHolder.DroneSettings.pitchRollCurve.Evaluate(pitchInput) * droneInfoHolder.DroneSettings.maxAngleOfAttack,
                0,
                droneInfoHolder.DroneSettings.pitchRollCurve.Evaluate(rollInput) * droneInfoHolder.DroneSettings.maxAngleOfAttack
            );
            Vector3 targetVelocity = new Vector3(
                0,
                droneInfoHolder.DroneSettings.liftCurve.Evaluate(liftInput) * droneInfoHolder.DroneSettings.maxLiftSpeed,
                0
            );
            Vector3 targetAngularSpeed = new Vector3(
                0, 
                droneInfoHolder.DroneSettings.yawCurve.Evaluate(yawInput) * droneInfoHolder.DroneSettings.maxAngularSpeed, 
                0);
            
            // Adjusts the speed of LIFT and the angular of YAW
            flightValues.Lift = droneInfoHolder.PIDController.AdjustLift(targetVelocity.y);
            flightValues.Yaw = droneInfoHolder.PIDController.AdjustYaw(targetAngularSpeed.y);
            
            // Brakes the drone when there is no pitch or roll input
            if (pitchInput == 0 && rollInput == 0)
            {
                // Use Cascade PID Controller:
                // Get the PID by Horizontal Speed (0,0) -> get Angle of Attack PID [Roll,Pitch]
                Vector2 speedPID = droneInfoHolder.PIDController.AdjustHorizontalSpeed(Vector2.zero);
            
                // Overwrite the input target Angle
                targetRotation = new Vector3(
                    speedPID.y * droneInfoHolder.DroneSettings.maxAngleOfAttack,   // Pitch
                    targetRotation.y,                                                  // Yaw
                    speedPID.x * droneInfoHolder.DroneSettings.maxAngleOfAttack    // Roll
                );
            }
            
            // PITCH ROLL
            Vector3 pitchRoll = droneInfoHolder.PIDController.AdjustAngleOfAttack(targetRotation);
            flightValues.Pitch = pitchRoll.x;
            flightValues.Roll = pitchRoll.z;
        }
    }
}