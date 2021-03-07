using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DriverWithFabscapeApiCall.Services
{
    public class ServerService: Driver.DriverBase
    {
        private readonly ILogger<ServerService> _logger;
        private readonly FabscapeApi _fabscapiApi;
        public ServerService(ILogger<ServerService> logger, FabscapeApi fabscapiApi)
        {
            _logger = logger;
            _fabscapiApi = fabscapiApi;
        }

        //public List<string> GetUsernames()
        //{
        //    var context = _fabscapiApi.CreateAuthTokenContext();
        //    var connection = _fabscapiApi.GetConnection();
        //    connection.ShutdownAsync().Wait();
        //}
    }
}
