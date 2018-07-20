using System;

namespace Retry.Logger
{
    /// <summary>
    /// Provides Message to logger
    /// </summary>
    public interface ITaskMessage
    {
        /// <summary>
        /// Send Message after every operation retry to the logger
        /// </summary>
        /// <param name="methodName">operation name being executed</param>
        /// <param name="retryCount">The number of times the operation is retried</param>
        string SetRetryCountMessage(string methodName,int retryCount);

        /// <summary>
        /// Send Message for duration to sleep after a failed invocation of the operation to logger
        /// </summary>
        /// <param name="methodName">operation name being executed</param>
        /// <param name="retryDelay">The duration to sleep after a failed invocation of the operation</param>
        string SetRetryDelayedMessage(string methodName,TimeSpan retryDelay);

        /// <summary>
        /// Send message when then the method execution started to logger
        /// </summary>
        /// <param name="methodName">operation name being executed</param>
        string SetMethodExecutionStartedMessage(string methodName);

        /// <summary>
        /// Send message after the operation is executed without any error to logger
        /// </summary>
        /// <param name="methodName">operation name being executed</param>
        string SetMethodExecutionSuccessMessage(string methodName);

        /// <summary>
        /// Send Message on operation execution failure to logger
        /// </summary>
        /// <param name="methodName">operation name being executed</param>
        string SetMethodExecutionFailureMessage(string methodName);

        /// <summary>
        /// Send Message after execution of the operation to logger
        /// </summary>
        /// <param name="methodName">operation name being executed</param>
        string SetMethodExecutionCompletedMessage(string methodName);
    }
}
