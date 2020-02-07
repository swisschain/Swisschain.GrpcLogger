using System;
using Autofac;
using Grpc.Net.Client;
using ProtoBuf.Grpc.Client;
using Swisschain.GrpcLogger.GrpcContracts;

namespace Swisschain.GrpcLogger
{
    public static class AutofacExtension
    {
        public static ContainerBuilder RegisterGrpcLogger(this ContainerBuilder builder, string serverUrl)
        {
            if (serverUrl == null) throw new ArgumentNullException(nameof(serverUrl));

            var client = GrpcChannel
                .ForAddress(serverUrl)
                .CreateGrpcService<ISystemLogService>();

            builder
                .RegisterType<GrpcPublisher>()
                .WithParameter("systemLogService", client)
                .AsSelf()
                .SingleInstance();

            builder
                .RegisterType<GrpcLoggerFactory>()
                .As<IGrpcLoggerFactory>()
                .SingleInstance();

            return builder;
        }

        public static ContainerBuilder RegisterGrpcLoggerLocal(this ContainerBuilder builder)
        {
            builder
                .RegisterType<GrpcPublisher>()
                .WithParameter("systemLogService", null)
                .AsSelf()
                .SingleInstance();

            builder
                .RegisterType<GrpcLoggerFactory>()
                .As<IGrpcLoggerFactory>()
                .SingleInstance();

            return builder;
        }
    }
}