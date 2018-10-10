using System;
using System.Collections.Generic;
using System.Linq;
using IntelliFlo.Platform;
using IntelliFlo.Platform.Http;
using IntelliFlo.Platform.Http.Documentation.Annotations;
using IntelliFlo.Platform.Http.ExceptionHandling;
using IntelliFlo.Platform.Http.ExceptionHandling.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Microservice.Platformer.v2.Controllers
{
    [Scope(Scopes.ClientData)] // Specific scope?
    [PublicApi]
    [NotFoundOnException(typeof(ResourceNotFoundException))]
    [BadRequestOnException(typeof(AssertionFailedException), typeof(BusinessException), typeof(ValidationException), typeof(ArgumentNullException))]
    [Route("v2/info")]
    public class InfoController : Controller
    {
        private readonly IConfiguration configuration;

        public InfoController(IConfiguration configuration)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        [HttpGet("config")]
        public IEnumerable<KeyValuePair<string, string>> GetConfig([FromQuery]string pattern)
        {
            return
                (from item in GetSettingsRecursive(configuration.GetChildren())
                 where string.IsNullOrEmpty(pattern) || item.Key.Contains(pattern, StringComparison.OrdinalIgnoreCase)
                 orderby item.Key
                 select item)
                .ToArray();
        }

        [HttpGet("vars")]
        public IEnumerable<KeyValuePair<string, string>> GetEnvironmentVariables([FromQuery]string pattern)
        {
            var variables = Environment.GetEnvironmentVariables();

            return
                (from string key in variables.Keys
                 where string.IsNullOrEmpty(pattern) || key.Contains(pattern, StringComparison.OrdinalIgnoreCase)
                 orderby key
                 select new KeyValuePair<string, string>(key, variables[key].ToString()))
                .ToArray();
        }

        private IEnumerable<KeyValuePair<string, string>> GetSettingsRecursive(IEnumerable<IConfigurationSection> configurationSections)
        {
            foreach (var child in configurationSections)
            {
                foreach (var grandChild in GetSettingsRecursive(child.GetChildren()))
                    yield return new KeyValuePair<string, string>(
                        grandChild.Key,
                        grandChild.Value);

                if (!child.GetChildren().Any())
                    yield return new KeyValuePair<string, string>(
                        child.Path,
                        child.Value);
            }
        }
    }
}
