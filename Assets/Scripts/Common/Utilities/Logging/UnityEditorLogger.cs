using UnityEngine;

namespace FpvDroneSimulator.Common.Utilities.Logging
{
    public class UnityEditorLogger : ILogger
    {
        public UnityEditorLogger(bool isEnabled = true)
        {
            IsEnabled = isEnabled;
        }

        public bool IsEnabled { get; set; }

        public void Log(string message, string prefixTag = null)
        {
            if (IsEnabled)
            {
                Debug.Log($"[{prefixTag}] {message}");
            }
        }

        public void LogError(string message, string prefixTag = null)
        {
            if (IsEnabled)
            {
                Debug.LogError($"[{prefixTag}] {message}");
            }
        }

        public void LogWarning(string message, string prefixTag = null)
        {
            if (IsEnabled)
            {
                Debug.LogWarning($"[{prefixTag}] {message}");
            }
        }

        public void LogAssertion(string message, string prefixTag = null)
        {
            if (IsEnabled)
            {
                Debug.LogAssertion($"[{prefixTag}] {message}");
            }
        }
    }
}