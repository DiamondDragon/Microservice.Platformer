using System;
using System.Threading.Tasks;
using IntelliFlo.Platform.Http;
using Monolith.Bulk.v2.Contracts;

namespace Monolith.Bulk.v2.Resources
{
    public interface IBulkImportResource : IResource
    {
        Task<BulkImportDocument> Get(Guid importId);
        Task<BulkImportDocumentCollection> GetAll(int top, int skip);
    }
}
