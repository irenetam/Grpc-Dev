using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DriverWithDatabase.Services
{
    public class DriverService : Driver.DriverBase
    {
        private readonly ILogger<DriverService> _logger;
        public DriverService(ILogger<DriverService> logger)
        {
            _logger = logger;
        }
    }
}
