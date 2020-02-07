using System.ServiceModel;
using System.Threading.Tasks;
using Swisschain.GrpcLogger.GrpcContracts.Contracts;
using Swisschain.SystemLog.GrpcContracts.Contracts;

namespace Swisschain.GrpcLogger.GrpcContracts
{
    [ServiceContract(Name = "SystemLog")]
    public interface ISystemLogService
    {
        [OperationContract(Action = "LogEvent")]
        ValueTask<LogEventResponse> RegisterAsync(LogEventRequest request);

        [OperationContract(Action = "GetEvents")]
        ValueTask<GetLogEventsResponse> GetEventsAsync(GetLogEventsRequest request);
    }
    
}