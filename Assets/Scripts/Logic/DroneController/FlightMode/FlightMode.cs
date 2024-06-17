namespace FpvDroneSimulator.Logic.DroneController
{
    public enum FlightMode
    {
        Angle, // Input is an angle of attack no greater than a limit.
        Manual, // No PID used, purely forces on each rotor
    }
}