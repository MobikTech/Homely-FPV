using FpvDroneSimulator.Common.Utilities.EventBus;
using FpvDroneSimulator.Logic;

namespace FpvDroneSimulator.Events
{
    public struct OnLevelSelected : IEvent
    {
        public GameLevel GameLevel;
        public GameLevelMode GameLevelMode;
    }
}