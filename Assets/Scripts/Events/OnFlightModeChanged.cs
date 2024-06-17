using FpvDroneSimulator.Common.Utilities.EventBus;
using FpvDroneSimulator.Logic.DroneController;

namespace FpvDroneSimulator.Events
{
    public struct OnFlightModeChanged : IEvent
    {
        public FlightMode FlightMode;
    }
}