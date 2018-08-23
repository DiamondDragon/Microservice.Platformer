using System.Collections.Generic;
using IntelliFlo.Platform.Http;
using IntelliFlo.Platform.Http.Documentation.Annotations;

namespace Monolith.Bulk.v2.Contracts
{
    [SwaggerDefinition("ImportCollection")]
    public class BulkImportDocumentCollection : CollectionResource<BulkImportDocument>
    {
        public BulkImportDocumentCollection(BulkImportDocument[] items, int totalCount) : base(items, totalCount){}

        public BulkImportDocumentCollection(params BulkImportDocument[] items) : base(items){}

        public BulkImportDocumentCollection(IEnumerable<BulkImportDocument> items, int totalCount) : base(items, totalCount){}
    }
}