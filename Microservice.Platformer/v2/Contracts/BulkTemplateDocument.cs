using System.ComponentModel;
using IntelliFlo.Platform.Http;
using IntelliFlo.Platform.Http.Documentation.Annotations;
using IntelliFlo.Platform.Http.HrefLinks;
using Newtonsoft.Json;

namespace Microservice.Platformer.v2.Contracts
{
    [SwaggerDefinition("BulkTemplate")]
    public class BulkTemplateDocument
    {
        /// <summary>
        /// Template identifier.
        /// </summary>
        [ReadOnly(true)]
        [AllowedVerbs(HttpVerbs.Get)]
        public string Id { get; set; }

        /// <summary>
        /// Template Name.
        /// </summary>
        [ReadOnly(true)]
        [AllowedVerbs(HttpVerbs.Get)]
        public string Name { get; set; }

        /// <summary>
        /// Short description of the template.
        /// </summary>
        [ReadOnly(true)]
        [AllowedVerbs(HttpVerbs.Get)]
        public string Description { get; set; }

        /// <summary>
        /// Duplication options. 
        /// </summary>
        [ReadOnly(true)]
        [AllowedVerbs(HttpVerbs.Get)]
        public EnumType<DuplicationOptions> DuplicationOptions { get; set; }

        /// <summary>
        /// Download url
        /// </summary>
        [ReadOnly(true)]
        [AllowedVerbs(HttpVerbs.Get)]
        [Href("v2/bulk/templates/{Id}")]
        [JsonProperty(PropertyName = "download_href")]
        public string DownloadHref { get; set; }
    }
}