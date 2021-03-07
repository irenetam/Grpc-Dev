using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;

namespace DriverWithFabscapeApiCall.Services
{
    public class FabscapeApi: Driver.DriverBase
    {
        private readonly ILogger<FabscapeApi> _logger;
        private readonly string pluginToken = "PLUGIN_TOKEN";
        private readonly ServerCallContext _context;
        public FabscapeApi(ILogger<FabscapeApi> logger, ServerCallContext context)
        {
            _logger = logger;
            _context = context;
        }

        public Channel GetConnection()
        {
            var apiEndpoint = "service-router:10000";
            Channel channel = null;
            try
            {
                channel = new Channel(apiEndpoint, ChannelCredentials.Insecure);
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Could not connect to the service-router {ex.Message}");
            }
            return channel;
        }

        public ServerCallContext CreateAuthTokenContext()
        {
            var token = Environment.GetEnvironmentVariable(pluginToken);
            _context.RequestHeaders.Add("authorization", token);
            return _context;
        }
    }
}
