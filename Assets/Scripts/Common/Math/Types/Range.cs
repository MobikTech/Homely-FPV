using System;
using UnityEngine;

namespace FpvDroneSimulator.Common.Math.Types
{
    [Serializable]
    public struct Range
    {
        [field:SerializeField] public int Min { get; private set; }
        [field:SerializeField] public int Max { get; private set; }

        public Range(int min, int max)
        {
            Min = min;
            Max = max;
        }
    }
}