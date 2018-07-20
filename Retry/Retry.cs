using Retry.Logger;
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
        public static ITaskMessage taskMessenger=new TaskMessage();
        
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
                        LogManager.LogRetryCountMessage(taskMessenger.SetRetryCountMessage(operation.Method.Name, retryCount));
                        LogManager.LogRetryDelayedMessage(taskMessenger.SetRetryDelayedMessage(operation.Method.Name, retryDelay));
                        
                        Thread.Sleep(retryDelay);
                    }

                    LogManager.LogMethodExecutionStartedMessage(taskMessenger.SetMethodExecutionStartedMessage(operation.Method.Name));
                    
                    // Execute method
                    operation();

                    //If the Control reaches this line then the Task is Success:
                    taskResult.Status = TaskStatus.Success;
                    LogManager.LogMethodExecutionSuccessMessage(taskMessenger.SetMethodExecutionSuccessMessage(operation.Method.Name));

                    break;
                }
                catch (Exception ex)
                {
                    LogManager.LogMethodExecutionFailureMessage(taskMessenger.SetMethodExecutionFailureMessage(operation.Method.Name));

                    if (!retryOnExceptionType.Select(x => x.GetType().FullName).Contains(ex.GetType().FullName))
                        break;
                }
            }

            LogManager.LogMethodExecutionCompletedMessage(taskMessenger.SetMethodExecutionCompletedMessage(operation.Method.Name));

            return taskResult;
        }
    }
}