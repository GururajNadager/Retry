using System;
using System.Collections.Generic;

namespace Retry
{
    public interface ITaskResult
    {
        IList<Exception> Exceptions { get; set; }

        TaskStatus Status { get; set; }
    }
}
