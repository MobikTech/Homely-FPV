using System;
using FpvDroneSimulator.Common.Core;
using FpvDroneSimulator.Logic;

namespace FpvDroneSimulator.GameStates.Options
{
    public class LevelResultStateOptions : IOptions
    {
        public GameLevel GameLevel;
        public TimeSpan SessionTimeSpent;
    }
}