using System;
using System.Threading.Tasks;
using IntelliFlo.Platform;
using IntelliFlo.Platform.Http;
using IntelliFlo.Platform.Http.Annotations;
using IntelliFlo.Platform.Http.Documentation.Annotations;
using Monolith.Bulk.v2.Resources;
using IntelliFlo.Platform.Http.ExceptionHandling;
using IntelliFlo.Platform.Http.ExceptionHandling.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Monolith.Bulk.v2.Controllers
{
    [Scope(Scopes.ClientData)]
    [AllowedAcceptHeaders("application/json")] // application/hal+json is not supported in v2
    [Authorize]
    [PublicApi]
    [BadRequestOnException(typeof(ValidationException))]
    [NotFoundOnException(typeof(ResourceNotFoundException))]
    [Route("v2/imports")]
    public class BulkImportController : Controller
    {
        private const int DefaultTop = FilteringConstants.Defaults.Top;
        private const int MaxTop = FilteringConstants.Defaults.MaxTop;

        private readonly IBulkImportResource bulkImportResource;

        public BulkImportController(IBulkImportResource bulkImportResource)
        {
            if (bulkImportResource == null)
                throw new ArgumentNullException(nameof(bulkImportResource));

            this.bulkImportResource = bulkImportResource;
        }

        /// <summary>
        /// Get a bulkImport by id.
        /// </summary>
        /// <param name="importId">BulkImport identifier</param>
        /// <returns>BulkImport document</returns>
        [HttpGet]
        [Route("{importId}")]
        public async Task<IActionResult> Get(Guid importId)
        {
            var result = await bulkImportResource.Get(importId);
            return Ok(result);
        }

        /// <summary>
        /// Returns a list of companies
        /// </summary>
        /// <param name="top">The number of recors to retrieve (default 25, max 100)</param>
        /// <param name="skip">Number of records to skip. Must be greater than or equal to zero</param>
        /// <returns>List of companies</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int top = DefaultTop, [FromQuery] int skip = 0)
        {
            if (top <= 0 || top > MaxTop)
                throw new BusinessException($"Top parameter value is out of range.");
            if (skip < 0)
                throw new BusinessException("Skip parameter value is out of range.");

            var result = await bulkImportResource.GetAll(top, skip);

            return Ok(result);
        }
    }
}