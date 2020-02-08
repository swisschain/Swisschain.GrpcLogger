using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProtoBuf.Grpc.Client;
using Swisschain.GrpcLogger.NetcoreLogger;

namespace Swisschain.GrpcLogger.ClientExample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            GrpcClientFactory.AllowUnencryptedHttp2 = true;
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile("appsettings.external.json", optional: true, reloadOnChange: true);
                })
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureLogging((context, logging) =>
                {
                    var host = context.Configuration.GetSection("GrpcLogging")?.GetSection("Host")?.Value;
                    GrpcLoggerFactoryStatic.Setup(host);

                    logging.ClearProviders();
                    logging.AddProvider(new GrpcLoggerProvider(GrpcLoggerFactoryStatic.Factory));
                    
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }


    
}
