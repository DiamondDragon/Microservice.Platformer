using System;
using System.Net;
using System.Threading.Tasks;
using IntelliFlo.Platform;
using IntelliFlo.Platform.Http;
using IntelliFlo.Platform.Http.Annotations;
using IntelliFlo.Platform.Http.Documentation.Annotations;
using IntelliFlo.Platform.Http.ExceptionHandling;
using IntelliFlo.Platform.Http.ExceptionHandling.Exceptions;
using IntelliFlo.Platform.Security;
using Microservice.Platformer.v2.Contracts;
using Microservice.Platformer.v2.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Microservice.Platformer.v2.Controllers
{
    [Scope(Scopes.ClientData)] // Specific scope?
    [Authorize(PolicyNames.ClientData)] // Specific scope?
    [PublicApi]
    [NotFoundOnException(typeof(ResourceNotFoundException))]
    [BadRequestOnException(typeof(AssertionFailedException), typeof(BusinessException), typeof(ValidationException), typeof(ArgumentNullException))]
    [Route("v2/bulk/templates")]
    public class BulkTemplateController : Controller
    {
        private readonly IBulkTemplateResource bulkTemplateResource;

        public BulkTemplateController(IBulkTemplateResource bulkTemplateResource)
        {
            this.bulkTemplateResource = bulkTemplateResource ?? throw new ArgumentNullException(nameof(bulkTemplateResource));
        }

        /// <summary>
        /// Download data import template by id
        /// </summary>
        /// <param name="templateId">Template identifier</param>
        /// <returns>CSV representation of the template</returns>
        [HttpGet]
        [AllowedAcceptHeaders("text/csv")]
        [SwaggerOperation("GetBulkTemplate")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "OK")]
        //[SwaggerResponse((int)HttpStatusCode.BadRequest, Description = "Bad Request")]
        [SwaggerResponse((int)HttpStatusCode.Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse((int)HttpStatusCode.Forbidden, Description = "Forbidden")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Description = "Not Found")]
        [Route("{templateId}")]
        [Produces("text/csv")]
        public async Task<IActionResult> Get(string templateId)
        {
            var stream = await bulkTemplateResource.DownloadAsync(templateId);

            return File(stream, "text/csv", templateId);
        }

        /// <summary>
        /// Returns a list of available templates
        /// </summary>
        /// <param name="top">The number of records to retrieve (default 100, max 500)</param>
        /// <param name="skip">Number of records to skip. Must be greater than or equal to zero</param>
        /// <returns>List of available templates</returns>
        [HttpGet]
        [AllowedAcceptHeaders("application/json")] // application/hal+json is not supported in v2
        [SwaggerOperation("ListBulkTemplates")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "OK", Type = typeof(BulkTemplateDocumentCollection))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Description = "Bad Request")]
        [SwaggerResponse((int)HttpStatusCode.Unauthorized, Description = "Unauthorized")]
        [SwaggerResponse((int)HttpStatusCode.Forbidden, Description = "Forbidden")]
        //[SwaggerResponse((int)HttpStatusCode.NotFound, Description = "Not Found")]
        [Route("")]
        [Produces("application/json")]
        public async Task<IActionResult> GetAll([FromQuery] int top = FilteringConstants.Defaults.Top, [FromQuery] int skip = 0)
        {
            var result = await bulkTemplateResource.GetAllAsync(top, skip);

            return Ok(result);
        }
    }
}