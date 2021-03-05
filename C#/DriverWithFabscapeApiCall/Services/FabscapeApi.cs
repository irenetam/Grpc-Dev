using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DriverWithFabscapeApiCall.Services
{
    public class FabscapeApi
    {
        ILogger<FabscapeApi> _logger;
        public FabscapeApi(ILogger<FabscapeApi> logger)
        {
            _logger = logger;
        }
    }
}
