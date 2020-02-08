Nuget Client library: [![NuGet Status](https://img.shields.io/nuget/v/Swisschain.GrpcLogger?color=green&label=Swisschain.GrpcLogger)](https://www.nuget.org/packages/Swisschain.GrpcLogger/)

# Swisschain.GrpcLogger

Library allows you to send logs to a remote server using the grpc protocol.

# How to use it with Autofac:

1. Register GrpcLogger in IOC container:

```code
public class Startup
{
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterGrpcLogger();
        }
}
```

2. Configurate logs for host builder in `program.cs`:
```code
        .ConfigureLogging((context, logging) =>
        {
            var host = context.Configuration.GetSection("GrpcLogging")?.GetSection("Host")?.Value;
            GrpcLoggerFactoryStatic.Setup(host);
            logging.ClearProviders();
            logging.AddProvider(new GrpcLoggerProvider(GrpcLoggerFactoryStatic.Factory));
        })
```

# Example library usage:
- [Client usage implementation](https://github.com/SC-Poc/Swisschain.GrpcLogger/tree/master/example/Swisschain.GrpcLogger.ClientExample)
- [Server implementation](https://github.com/SC-Poc/Swisschain.GrpcLogger/tree/master/example/Swisschain.GrpcLogger.ServerExample)
