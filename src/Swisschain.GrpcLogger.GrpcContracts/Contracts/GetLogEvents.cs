using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Swisschain.GrpcLogger.GrpcContracts.Models;

namespace Swisschain.GrpcLogger.GrpcContracts.Contracts
{
    [DataContract]
    public class GetLogEventsRequest
    {
        
        [DataMember(Order = 1)]
        public DateTime DateFrom { get; set; }
        
        [DataMember(Order = 2)]
        public DateTime DateTo { get; set; }
        
        [DataMember(Order = 3)]
        public IEnumerable<string> Components { get; set; }
        
    }


    [DataContract]
    public class GetLogEventsResponse
    {
        [DataMember(Order = 1)]
        public IEnumerable<LogEventModel> Events { get; set; }
    }
}