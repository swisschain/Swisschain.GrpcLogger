using System;
using Microsoft.Extensions.Logging;

namespace Swisschain.GrpcLogger.NetcoreLogger
{
    public class GrpcLogger : ILogger
    {
        private readonly IGrpcLogger _logger;

        public GrpcLogger(IGrpcLogger logger)
        {
            _logger = logger;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            var message = formatter(state, exception);

            switch (logLevel)
            {
                case LogLevel.None:
                case LogLevel.Trace:
                case LogLevel.Debug:
                case LogLevel.Information:
                    _logger.Info(string.Empty, message, exception);
                    break;

                case LogLevel.Warning:
                    _logger.Warning(string.Empty, message, exception);
                    break;

                case LogLevel.Error:
                case LogLevel.Critical:
                    _logger.Error(string.Empty, message, exception);
                    break;
            }
        }
    }
}