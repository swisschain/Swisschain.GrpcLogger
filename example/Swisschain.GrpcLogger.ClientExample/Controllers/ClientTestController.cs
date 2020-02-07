using System;
using Microsoft.AspNetCore.Mvc;

namespace Swisschain.GrpcLogger.ClientExample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientTestController : ControllerBase
    {
        private readonly IGrpcLogger _log;

        public ClientTestController(IGrpcLoggerFactory loggerFactory)
        {
            _log = loggerFactory.GetLogger(this);
        }

        [HttpGet]
        public void Post([FromQuery]string message)
        {
            _log.Info("Post process", message);
            _log.Warning("Post process", message);
            _log.WriteError("Post process", new Exception(message));
            _log.WriteError("Post process", message, new Exception(message));
        }
    }
}
