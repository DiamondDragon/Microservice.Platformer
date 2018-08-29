using System.IO;
using System.Threading.Tasks;
using Microservice.Platformer.v2.Contracts;

namespace Microservice.Platformer.v2.Resources
{
    public class BulkTemplateResource : IBulkTemplateResource
    {
        public async Task<Stream> DownloadAsync(string templateId)
        {
            return new MemoryStream();
        }

        public async Task<BulkTemplateDocumentCollection> GetAllAsync(int top, int skip)
        {
            return new BulkTemplateDocumentCollection();
        }
    }
}