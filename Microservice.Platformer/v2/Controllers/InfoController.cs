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
        public object GetConfig([FromQuery]string pattern)
        {
            return
                (from item in GetSettingsRecursive(configuration.GetChildren())
                 where string.IsNullOrEmpty(pattern) || item.Key.Contains(pattern, StringComparison.OrdinalIgnoreCase)
                 orderby item.Key
                 select new
                 {
                     Id = 111,
                     Key = item.Key,
                     Value = item.Value,
                     Details = new
                     {
                         Key = 123,
                         Value = 123
                     }
                 })
                .ToArray();
        }

        [HttpGet("vars")]
        public IEnumerable<object> GetEnvironmentVariables([FromQuery]string pattern)
        {
            var variables = Environment.GetEnvironmentVariables();

            return
                (from string key in variables.Keys
                 where string.IsNullOrEmpty(pattern) || key.Contains(pattern, StringComparison.OrdinalIgnoreCase)
                 orderby key
                 select new
                 {
                     Id = 111,
                     Key = key,
                     Value = variables[key].ToString(),
                     Details = new
                     {
                         Key = 123,
                         Value = 123
                     }
                 })
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

        private async Task<Dto[]> ReadData()
        {
            using (var client = serviceClientFactory.Create("Platformer"))
            {
                return (await client.Get<Dto[]>("v2/info/vars")).Resource;
            }
        }


        public class Dto
        {
            public int Id { get; set; }
            public string Key { get; set; }
            public string Name { get; }
            public string Value { get; set; }
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
