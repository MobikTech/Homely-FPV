using UnityEngine;

namespace FpvDroneSimulator.Logic.DroneController
{
    public class AngleFlightValuesProvider : FlightValuesProviderBase
    {
        public override void ApplyInput(FlightValues flightValues)
        {
  
            Vector2 targetAngleOfAttack = new Vector2(
                droneInfoHolder.DroneSettings.pitchRollCurve.Evaluate(pitchInput), 
                droneInfoHolder.DroneSettings.pitchRollCurve.Evaluate(rollInput));
            targetAngleOfAttack *= droneInfoHolder.DroneSettings.maxAngleOfAttack;
            
            if (droneInfoHolder.PIDController.enabled)
            {
                Vector2 pitchAndRollCorrection = droneInfoHolder.PIDController.AdjustAngleOfAttack(new Vector2(targetAngleOfAttack.x, targetAngleOfAttack.y));
                flightValues.Pitch = pitchAndRollCorrection.x;
                flightValues.Roll = pitchAndRollCorrection.y;
            }
                    
            flightValues.Yaw = droneInfoHolder.DroneSettings.yawCurve.Evaluate(yawInput);
            flightValues.Lift = droneInfoHolder.DroneSettings.liftCurve.Evaluate(liftInput);
        }
    }
}