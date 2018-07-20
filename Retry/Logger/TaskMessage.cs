using System;

namespace Retry.Logger
{
    public class TaskMessage : ITaskMessage
    {
        public string SetMethodExecutionCompletedMessage(string methodName)
        {
            return $"Method Execution {methodName} completed at {DateTime.Now}";
        }

        public string SetMethodExecutionFailureMessage(string methodName)
        {
            return $"Method Execution {methodName} failed";
        }

        public string SetMethodExecutionStartedMessage(string methodName)
        {
            return $"Started Executing Method {methodName} at {DateTime.Now}";
        }

        public string SetMethodExecutionSuccessMessage(string methodName)
        {
            return $"Method Execution {methodName} success";
        }

        public string SetRetryCountMessage(string methodName,int retryCount)
        {
            return $"Retrying Method Execution {methodName} :retry attempt ({retryCount} of {retryCount - 1})";
        }

        public string SetRetryDelayedMessage(string methodName,TimeSpan retryDelay)
        {
            return $"Retrying Method Execution {methodName} in {retryDelay.Hours} hour {retryDelay.Minutes} minutes {retryDelay.Seconds} seconds";
        }
    }
}
