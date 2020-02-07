using System.Collections.Generic;
using System.Runtime.Serialization;
using Swisschain.GrpcLogger.GrpcContracts.Models;

namespace Swisschain.SystemLog.GrpcContracts.Contracts
{

    [DataContract]
    public class LogEventRequest
    {
        
        [DataMember(Order = 1)]
        public string Component { get; set; }
        
        [DataMember(Order = 2)]
        public IEnumerable<LogEventModel> Events { get; set; }

    }
    
    [DataContract]
    public class LogEventResponse
    {
        [DataMember(Order = 1)]
        public string Component { get; set; }
    }
    
}