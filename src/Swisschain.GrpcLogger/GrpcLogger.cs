using System;
using Swisschain.GrpcLogger.GrpcContracts.Models;
using Swisschain.SystemLog;

namespace Swisschain.GrpcLogger
{
    public class GrpcLogger : IGrpcLogger
    {
        private readonly GrpcPublisher _publisher;
        private readonly string _component;


        public GrpcLogger(GrpcPublisher publisher, object componentName)
        {
            _publisher = publisher;
            _component = componentName.ToString();
        }

        public void Info(string process, string message, Exception exception = null)
        {
            _publisher.EnqueueEvent(new LogEventModel
            {
                DateTime = DateTime.UtcNow,
                Message = message,
                Process = process,
                LogLevel = LogLevel.Info,
                Component = _component,
                StackTrace = exception?.StackTrace,
                Exception = exception != null ? $"{exception.GetType().Name} {exception.Message}" : null
            });
        }

        public void Warning(string process, string message, Exception exception = null)
        {
            _publisher.EnqueueEvent(new LogEventModel
            {
                DateTime = DateTime.UtcNow,
                Message = message,
                Process = process,
                LogLevel = LogLevel.Warning,
                Component = _component,
                StackTrace = exception?.StackTrace,
                Exception = exception != null ? $"{exception.GetType().Name} {exception.Message}" : null
            });
        }
        
        public void Error(string process, Exception exception)
        {
            _publisher.EnqueueEvent(new LogEventModel
            {
                DateTime = DateTime.UtcNow,
                Message = "",
                Process = process,
                LogLevel = LogLevel.Error,
                Component = _component,
                StackTrace = exception?.StackTrace,
                Exception = exception != null ? $"{exception.GetType().Name} {exception.Message}" : null,
            });
        }

        public void Error(string process, string message, Exception exception = null)
        {
            _publisher.EnqueueEvent(new LogEventModel
            {
                DateTime = DateTime.UtcNow,
                Message = message,
                Process = process,
                StackTrace = exception?.StackTrace,
                LogLevel = LogLevel.Error,
                Component = _component,
                Exception = exception != null ? $"{exception.GetType().Name} {exception.Message}" : null
            });
        }








    }
}