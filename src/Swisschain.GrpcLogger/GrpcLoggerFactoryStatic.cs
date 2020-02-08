using System;
using Grpc.Net.Client;
using ProtoBuf.Grpc.Client;
using Swisschain.GrpcLogger.GrpcContracts;

namespace Swisschain.GrpcLogger
{
    public static class GrpcLoggerFactoryStatic
    {
        private static IGrpcLoggerFactory _factory;

        public static void Setup(string serverUrl)
        {
            if (_factory != null)
                return;

            var client = !string.IsNullOrEmpty(serverUrl)
                ? GrpcChannel
                    .ForAddress(serverUrl)
                    .CreateGrpcService<ISystemLogService>()
                : null;

            var publisher = new GrpcPublisher(client);

            _factory = new GrpcLoggerFactory(publisher);
        }

        public static IGrpcLoggerFactory Factory => _factory ?? throw new Exception("Please setup GrpcLoggerFactoryStatic before start use log system");
    }
}