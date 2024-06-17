using FpvDroneSimulator.Common.Utilities.EventBus;

namespace FpvDroneSimulator.Events
{
    public struct OnRaceProgressChanged : IEvent
    {
        public int CurrentLap;
        public int MaxLaps;
        public int CurrentCheckpoint;
        public int MaxCheckpoints;
    }
}