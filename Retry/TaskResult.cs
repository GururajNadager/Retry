using System;
using System.Collections.Generic;

namespace Retry
{
    public class TaskResult : ITaskResult
    {
        public TaskResult()
        {
            Exceptions = new List<Exception>();
            Status = TaskStatus.Failure;
        }
        public IList<Exception> Exceptions { get; set; }

        public TaskStatus Status { get; set; }
    }
}
