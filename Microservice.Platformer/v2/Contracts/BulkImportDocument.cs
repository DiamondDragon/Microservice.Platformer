using System;
using IntelliFlo.Platform.Http.Documentation.Annotations;
using IntelliFlo.Platform.Http.HrefLinks;

namespace Monolith.Bulk.v2.Contracts
{
    [SwaggerDefinition("Import")]
    public class BulkImportDocument
    {
        public Guid Id { get; set; }

        [Href("v2/imports/{Id}")]
        public string Href { get; set; }

        public DateTime UploadedAt { get; set; }

        public string Status { get; set; }

        public int RecordCount { get; set; }

        public string Message { get; set; }
    }
}