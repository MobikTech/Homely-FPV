namespace FpvDroneSimulator.Common.Utilities.Logging
{
    public static class LoggerExtensions
    {
        public static void Log(this ILogger logger, string message, LogCategory category)
        {
            logger.Log(message, category.ToString());
        }

        public static void LogError(this ILogger logger, string message, LogCategory category)
        {
            logger.LogError(message, category.ToString());
        }

        public static void LogWarning(this ILogger logger, string message, LogCategory category)
        {
            logger.LogWarning(message, category.ToString());
        }

        public static void LogAssertion(this ILogger logger, string message, LogCategory category)
        {
            logger.LogAssertion(message, category.ToString());
        }
    }
}