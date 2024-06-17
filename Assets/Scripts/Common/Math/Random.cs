using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using FpvDroneSimulator.Common.Math.Types;

namespace FpvDroneSimulator.Common.Math
{
    public static class Random
    {
        public static int RandomEven(int minIncl, int maxIncl)
        {
            int answer = UnityEngine.Random.Range(minIncl, maxIncl + 1);
            if (answer % 2 != 0)
            {
                return answer + 1 <= maxIncl ? answer + 1 : answer - 1;
            }
            
            return answer;
        }
        public static int RandomEven(Range range) => RandomEven(range.Min, range.Max);

        public static int RandomOdd(int minIncl, int maxIncl)
        {
            int answer = UnityEngine.Random.Range(minIncl, maxIncl + 1);
            if (answer % 2 == 0)
            {
                return answer + 1 <= maxIncl ? answer + 1 : answer - 1;
            }
            
            return answer;
        }
        public static int RandomOdd(Range range) => RandomOdd(range.Min, range.Max);


        public static int FromRange(int minIncl, int maxIncl) => UnityEngine.Random.Range(minIncl, maxIncl + 1);
        public static int FromRange(Range range) => UnityEngine.Random.Range(range.Min, range.Max + 1);
        
        public static bool IsTruth(float probability)
        {
            if (probability > 1f || probability < 0f)
                throw new InvalidEnumArgumentException("Probability should be in range from 0 to 1");

            return UnityEngine.Random.value <= probability;
        }

        public static (int index, TElement value) FromArray<TElement>(IEnumerable<TElement> array)
        {
            int index = UnityEngine.Random.Range(0, array.Count());
            return (index, array.ElementAt(index));
        }
    }
}