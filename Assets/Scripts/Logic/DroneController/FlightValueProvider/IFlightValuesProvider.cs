namespace FpvDroneSimulator.Logic.DroneController
{
    public interface IFlightValuesProvider
    {
        public void ApplyInput(FlightValues flightValues);
    }
}