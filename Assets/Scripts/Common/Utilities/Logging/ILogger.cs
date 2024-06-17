namespace FpvDroneSimulator.Common.Utilities.Logging
{
    public interface ILogger
    {
        public bool IsEnabled { get; set; }

        public void Log(string message, string prefixTag = null);
        public void LogError(string message, string prefixTag = null);
        public void LogWarning(string message, string prefixTag = null);
        public void LogAssertion(string message, string prefixTag = null);
    }
}