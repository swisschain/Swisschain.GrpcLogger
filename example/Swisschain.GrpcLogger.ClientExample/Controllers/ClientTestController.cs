using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Swisschain.GrpcLogger.ClientExample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientTestController : ControllerBase
    {
        private readonly ILogger<ClientTestController> _logger;
        private readonly IGrpcLogger _log;

        public ClientTestController(IGrpcLoggerFactory loggerFactory, ILogger<ClientTestController> logger)
        {
            _logger = logger;
            _log = loggerFactory.GetLogger(this);
        }

        [HttpGet("grpc_logger")]
        public void GrpcLogger([FromQuery]string message)
        {
            _log.Info("Post process", message);
            _log.Warning("Post process", message);
            _log.Error("Post process", new Exception(message));
            _log.Error("Post process", message, new Exception(message));
        }

        [HttpGet("netcore_logger")]
        public void NetCoreLogger([FromQuery]string message)
        {
            _logger.LogInformation(message);
            _logger.LogInformation(new Exception(message), message);
            _logger.LogWarning(message);
            _logger.LogError(new Exception(message), message);
            _logger.LogError(message);
        }
    }
}
