using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace ProjectVotestorm.UnitTests.Helpers
{
    public class MockLogger<T> : ILogger<T>, IDisposable
    {
        public List<string> Logs { get; } = new List<string>();

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            Logs.Add(state.ToString());
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return this;
        }

        public void Dispose() {}
    }
}
