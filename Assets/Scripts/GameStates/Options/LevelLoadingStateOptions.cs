using FpvDroneSimulator.Common.Core;
using FpvDroneSimulator.Logic;

namespace FpvDroneSimulator.GameStates.Options
{
    public class LevelLoadingStateOptions : IOptions
    {
        public GameLevel GameLevel;
        public GameLevelMode GameLevelMode;
    }
}