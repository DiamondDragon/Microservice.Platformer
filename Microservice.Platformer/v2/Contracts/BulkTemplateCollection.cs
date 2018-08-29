using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntelliFlo.Platform.Http;
using IntelliFlo.Platform.Http.Documentation.Annotations;

namespace Microservice.Platformer.v2.Contracts
{
    [SwaggerDefinition("BulkTemplateCollection")]
    public class BulkTemplateDocumentCollection : CollectionResource<BulkTemplateDocument>
    {
        public BulkTemplateDocumentCollection(BulkTemplateDocument[] items, int totalCount) : base(items, totalCount) { }

        public BulkTemplateDocumentCollection(params BulkTemplateDocument[] items) : base(items) { }

        public BulkTemplateDocumentCollection(IEnumerable<BulkTemplateDocument> items, int totalCount) : base(items, totalCount) { }
    }
}
