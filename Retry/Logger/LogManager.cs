using System.Collections.Generic;

namespace Retry.Logger
{
    public static class LogManager
    {
        public static IList<ITaskLogger> Loggers { get; set; }

        /// <summary>
        /// Log Retry Count Message to the output
        /// </summary>
        /// <param name="message">message to be written in the output</param>
        public static void LogRetryCountMessage(string message)
        {
            LogInfo(message);
        }

        /// <summary>
        /// Log Retry Delayed Message to the output
        /// </summary>
        /// <param name="message">message to be written in the output</param>
        public static void LogRetryDelayedMessage(string message)
        {
            LogInfo(message);
        }

        /// <summary>
        /// Log Method Execution Started Message to the output
        /// </summary>
        /// <param name="message">message to be written in the output</param>
        public static void LogMethodExecutionStartedMessage(string message)
        {
            LogInfo(message);
        }

        /// <summary>
        /// Log Method Execution Success Message to the output
        /// </summary>
        /// <param name="message">message to be written in the output</param>
        public static void LogMethodExecutionSuccessMessage(string message)
        {
            LogInfo(message);
        }

        /// <summary>
        /// Log Method Execution Failure Message to the output
        /// </summary>
        /// <param name="message">message to be written in the output</param>
        public static void LogMethodExecutionFailureMessage(string message)
        {
            LogInfo(message);
        }

        /// <summary>
        /// Log Method Execution Completed Message to the output
        /// </summary>
        /// <param name="message">message to be written in the output</param>
        public static void LogMethodExecutionCompletedMessage(string message)
        {
            LogInfo(message);
        }

        private static void LogInfo(string message)
        {
            foreach (var logger in Loggers)
            {
                logger.LogInfo(message);
            }
        }
    }
}
