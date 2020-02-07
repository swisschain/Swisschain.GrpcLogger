using System;
using System.Runtime.Serialization;

namespace Swisschain.GrpcLogger.GrpcContracts.Models
{
    
    public enum LogLevel
    {
        Error, Warning, Info
    }
    
    [DataContract]
    public class LogEventModel
    {
        [DataMember(Order = 1)]
        public DateTime DateTime { get; set; }
        
        [DataMember(Order = 2)]
        public LogLevel LogLevel { get; set; }

        [DataMember(Order = 3)]
        public string Component { get; set; }

        [DataMember(Order = 4)]
        public string Process { get; set; }
        
        [DataMember(Order = 5)]
        public string Message { get; set; }

        [DataMember(Order = 6)]
        public string StackTrace { get; set; }

        [DataMember(Order = 7)]
        public string Exception { get; set; }

        [DataMember(Order = 8)]
        public string AppName { get; set; }

        [DataMember(Order = 9)]
        public string AppVersion { get; set; }
    }
}