using System;
using System.IO;
using System.Threading.Tasks;
using IntelliFlo.Platform;
using IntelliFlo.Platform.Http;
using IntelliFlo.Platform.Http.ExceptionHandling.Exceptions;
using Microservice.Platformer.v2.Contracts;

namespace Microservice.Platformer.v2.Resources
{
    public interface IBulkTemplateResource : IResource
    {
        Task<Stream> DownloadAsync(string templateId);

        Task<BulkTemplateDocumentCollection> GetAllAsync(int top, int skip);
    }
}