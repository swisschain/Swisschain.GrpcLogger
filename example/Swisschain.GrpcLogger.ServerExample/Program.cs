using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Hosting;

namespace Swisschain.GrpcLogger.ServerExample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel(options =>
                    {
                         // Setup a HTTP / 2 endpoint without TLS.
                         options.Listen(IPAddress.Any, 5000, o => o.Protocols = HttpProtocols.Http2);
                         options.Listen(IPAddress.Any, 5001, o => o.Protocols = HttpProtocols.Http1);
                    });

                    webBuilder.UseStartup<Startup>();
                });
    }
}
