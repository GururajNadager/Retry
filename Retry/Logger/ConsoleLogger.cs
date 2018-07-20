using System;

namespace Retry.Logger
{
    public class ConsoleLogger : ITaskLogger
    {
        public void LogError(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        public void LogInfo(string message)
        {
            Console.WriteLine(message);
        }
    }
}
