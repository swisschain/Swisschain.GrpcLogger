using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;

namespace Swisschain.GrpcLogger.NetcoreLogger
{
    public class GrpcLoggerProvider : ILoggerProvider
    {
        private readonly IGrpcLoggerFactory _factory;
        private ConcurrentDictionary<string, GrpcLogger> _loggers = new ConcurrentDictionary<string, GrpcLogger>();

        public GrpcLoggerProvider(IGrpcLoggerFactory factory)
        {
            _factory = factory;
        }

        public void Dispose()
        {
            _loggers.Clear();
        }

        public ILogger CreateLogger(string categoryName)
        {
            return _loggers.GetOrAdd(categoryName, name => new GrpcLogger(_factory.GetLogger(categoryName)));
        }
    }
}