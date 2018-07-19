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
                        Thread.Sleep(retryDelay);
                    }

                    // Execute method
                    operation();

                    //If the Control reaches this line then the Task is Success:
                    taskResult.Status = TaskStatus.Success;
                    break;
                }
                catch (Exception ex)
                {
                    taskResult.Exceptions.Add(ex);

                    if (!retryOnExceptionType.Select(x => x.InnerException.GetType()).Contains(ex.GetType()))
                        break;
                }
            }

            return taskResult;
        }
    }
}