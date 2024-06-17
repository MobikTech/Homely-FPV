namespace FpvDroneSimulator.Logic.DroneController
{
    public class ManualFlightValuesProvider : FlightValuesProviderBase
    {
        public override void ApplyInput(FlightValues flightValues)
        {
            // In Manual no PID is used, only curves are processed.
            flightValues.Lift = droneInfoHolder.DroneSettings.liftCurve.Evaluate(liftInput);
            flightValues.Pitch = droneInfoHolder.DroneSettings.yawCurve.Evaluate(pitchInput);
            flightValues.Roll = droneInfoHolder.DroneSettings.yawCurve.Evaluate(rollInput);
            flightValues.Yaw = droneInfoHolder.DroneSettings.yawCurve.Evaluate(yawInput);
        }
    }
}