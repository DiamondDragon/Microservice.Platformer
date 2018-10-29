using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntelliFlo.Platform;
using IntelliFlo.Platform.Client;
using IntelliFlo.Platform.Http;
using IntelliFlo.Platform.Http.Documentation.Annotations;
using IntelliFlo.Platform.Http.ExceptionHandling;
using IntelliFlo.Platform.Http.ExceptionHandling.Exceptions;
using Microservice.Platformer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

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
        private readonly IBulkImportService importService;
        private readonly IServiceClientFactory serviceClientFactory;

        public InfoController(
            IConfiguration configuration,
            IBulkImportService importService,
            IServiceClientFactory serviceClientFactory)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this.importService = importService ?? throw new ArgumentNullException(nameof(importService));
            this.serviceClientFactory = serviceClientFactory ?? throw new ArgumentNullException(nameof(serviceClientFactory));
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

        [HttpGet("dbtest")]
        [HttpPost("dbtest")]
        public IActionResult DbTest()
        {
            importService.AddData();
            importService.GetData();;

            return Ok();
        }


        [HttpGet("dbtest-async")]
        [HttpPost("dbtest-async")]
        public async Task<IActionResult> DbTestAsync()
        {
            await importService.AddDataAsync();
            await importService.GetDataAsync(); ;

            return Ok();
        }

        [HttpGet("collaborator-test")]
        public async Task<IActionResult> CollaboratorTest()
        {
            var data = await ReadData();

            return Ok(data);
        }

        private async Task<KeyValuePair<string, string>[]> ReadData()
        {
            using (var client = serviceClientFactory.Create("Platformer"))
            {
                return (await client.Get<KeyValuePair<string, string>[]>("v2/info/vars")).Resource;
            }
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
