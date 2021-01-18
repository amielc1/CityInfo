using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Services
{
    public class LocalMailService : IMailService
    {
        private readonly ILogger<LocalMailService> _logger;
        private readonly IConfiguration _configuration;

        public LocalMailService(ILogger<LocalMailService> logger, IConfiguration configuration)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }
        public void Send(string subject, string message)
        {
            _logger.LogInformation("Send LocalMailService");
            _logger.LogInformation($"From : {_configuration["mailSettings:mailFromAddress"]} , To {_configuration["mailSettings:mailToAddress"]}");
            _logger.LogInformation($"Subject : {subject}");
            _logger.LogInformation($"Message : {message}");
        }
    }
}
