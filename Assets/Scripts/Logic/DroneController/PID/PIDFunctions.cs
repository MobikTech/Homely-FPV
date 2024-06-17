using UnityEngine;

// This class gathers all PID algorithms.
namespace FpvDroneSimulator.Logic.DroneController.PID
{
    public static class PIDFunctions
    {
        // Result is in [-1,1]
        public static float GetPIDResult(PID_Configuration config, float error, float errorSum, float lastFrameError)
        {
            if (error is > 1 or < -1)
            {
                Debug.LogError("Error is out of range [-1,1]: " + config.name + " = " + error);
                return 0;
            }

            float deltaTime = Time.inFixedTimeStep ? Time.fixedDeltaTime : Time.deltaTime;

            float p = Proportional(config.pGain, error);
            float i = Integral(config.iGain, error, errorSum);
            float d = Derivative(config.dGain, error, lastFrameError, deltaTime);

            return Mathf.Clamp(p + i + d, -1, 1);
        }


        // P -> Proportional Error (Present)
        private static float Proportional(float pGain, float error)
        {
            return pGain * error;
        }

        // I -> Integral Error (Past)
        private static float Integral(float iGain, float error, float errorSum)
        {
            return iGain * AntiWindupFilter(errorSum, error);
        }

        // D -> Derivative Error (Future)
        private static float Derivative(float dGain, float error, float lastError, float deltaTime)
        {
            return dGain * (error - lastError) / deltaTime;
        }

        // Anti Integral Windup Clamping Filter:
        // Clamp Integral Error if its out of saturation values [-1,1] & its not reducing the error
        public static float AntiWindupFilter(float sum, float error)
        {
            bool saturated = sum > 1 || sum < -1;
            bool sameSign = Mathf.Abs(Mathf.Sign(sum) - Mathf.Sign(error)) < 0.00001f;
            return saturated && sameSign ? 0 : sum;
        }
    }
}