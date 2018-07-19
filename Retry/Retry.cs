using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Retry
{
    /// <summary>
    /// This class provides a generic Retry Pattern to handle transient faults
    /// </summary>
    public static class Retry
    {
        /// <summary>
        /// When Enabled,Calls the Appropriate Logger Method
        /// </summary>
        public static bool EnableLogging { get; set; }

        /// <summary>
        /// Delegate Method for Info Logger Method
        /// </summary>
        public static Action<string> InfoLogAction { get; set; }

        /// <summary>
        /// Delegate Method for Exception Logger Method
        /// </summary>
        public static Action<Exception> ExceptionLogAction { get; set; }

        /// <summary>
        /// Retry the specified operation the specified number of times, until there are no more retries or it succeeded
        /// without an exception.
        /// </summary>
        /// <param name="operation">The operation to perform</param>
        /// <param name="retryDelay">The duration to sleep after a failed invocation of the operation</param>
        /// <param name="retryCount">The number of times to retry the operation</param>
        /// <returns>When one of the retries succeeds, return the TaskResult with Status Success.
        /// If not, return the TaskResult with Status Failure with caught Exceptions.</returns>
        public static ITaskResult Execute(Action operation, TimeSpan retryDelay, int retryCount)
        {
            return Execute(operation, retryDelay, retryCount, new List<Exception>());
        }

        /// <summary>
        ///Retry the specified operation the specified number of times, until there are no more retries or it succeeded
        /// without an exception.The retry is attempted only if the Exception matches the retryException Types argument
        /// </summary>
        /// <param name="operation">The operation to perform</param>
        /// <param name="retryDelay">The duration to sleep after a failed invocation of the operation</param>
        /// <param name="retryCount">The number of times to retry the operation</param>
        /// <param name="retryExceptions">The operation will retry only if exception matches the argument retryException Types</param>
        /// <returns>When one of the retries succeeds, return the TaskResult with Status Success.
        /// If not, return the TaskResult with Status Failure with caught Exceptions.</returns>
        public static ITaskResult Execute(Action operation, TimeSpan retryDelay, int retryCount,IList<Exception> retryOnExceptionType)
        {
            ITaskResult taskResult = new TaskResult();

            for (int retry = 0; retry < retryCount; retry++)
            {
                try
                {
                    if (retry > 0)
                    {
                        LogInfo($"Retrying Method Execution {operation.Method.Name} :retry attempt ({retry} of {retry-1})");

                        Thread.Sleep(retryDelay);

                        LogInfo($"Retrying Method Execution {operation.Method.Name} in {retryDelay.Hours} hour {retryDelay.Minutes} minutes {retryDelay.Seconds} seconds");
                    }

                    LogInfo($"Started Executing Method {operation.Method.Name} at {DateTime.Now}");

                    // Execute method
                    operation();

                    //If the Control reaches this line then the Task is Success:
                    taskResult.Status = TaskStatus.Success;
                    LogInfo($"Method Execution {operation.Method.Name} success");

                    break;
                }
                catch (Exception ex)
                {
                    LogInfo($"Method Execution {operation.Method.Name} failed");

                    if (!retryOnExceptionType.Select(x => x.GetType().FullName).Contains(ex.GetType().FullName))
                        break;
                }
            }

            LogInfo($"Method Execution {operation.Method.Name} completed at {DateTime.Now}");

            return taskResult;
        }

        /// <summary>
        /// Calls the Logger Actions if Logging is enabled
        /// </summary>
        /// <param name="message">message to log</param>
        private static void LogInfo(string message)
        {
            if(EnableLogging)
                InfoLogAction(message);
        }
    }
}