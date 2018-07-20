using System;

namespace Retry.Logger
{
    /// <summary>
    /// Contract for Logger Implementation
    /// </summary>
    public interface ITaskLogger
    {
        /// <summary>
        /// Log Inforamtion to the output
        /// </summary>
        /// <param name="message">message to be printed</param>
        void LogInfo(string message);

        /// <summary>
        /// Log Exception to the output
        /// </summary>
        /// <param name="ex">Exception to be printed</param>
        void LogError(Exception ex);
    }
}
