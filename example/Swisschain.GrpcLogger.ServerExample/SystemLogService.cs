using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Swisschain.GrpcLogger.GrpcContracts;
using Swisschain.GrpcLogger.GrpcContracts.Contracts;
using Swisschain.GrpcLogger.GrpcContracts.Models;
using Swisschain.SystemLog.GrpcContracts.Contracts;

namespace Swisschain.GrpcLogger.ServerExample
{
    public class SystemLogService : ISystemLogService
    {
        public async ValueTask<LogEventResponse> RegisterAsync(LogEventRequest request)
        {
            foreach (var log in request.Events)
            {
                Console.WriteLine($"{log.DateTime:s} {log.LogLevel}: {log.Component};{log.Process};{log.Message}");
            }

            return new LogEventResponse(){Component = request.Component};
        }

        public async ValueTask<GetLogEventsResponse> GetEventsAsync(GetLogEventsRequest request)
        {
            return new GetLogEventsResponse()
            {
                Events = new List<LogEventModel>()
            };
        }
    }
}