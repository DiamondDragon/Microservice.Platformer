using System;
using System.Threading.Tasks;
using IntelliFlo.Platform.Http;
using Microservice.Platformer.v2.Contracts;

namespace Microservice.Platformer.v2.Resources
{
    public interface IBulkImportResource : IResource
    {
        Task<BulkImportDocument> Get(Guid importId);
        Task<BulkImportDocumentCollection> GetAll(int top, int skip);
    }
}
