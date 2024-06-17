using System;
using System.Collections.Generic;

namespace FpvDroneSimulator.Logic.LevelProgress.Models
{
    [Serializable]
    public struct LapInfo
    {
        public List<Checkpoint> Checkpoints;
    }
}