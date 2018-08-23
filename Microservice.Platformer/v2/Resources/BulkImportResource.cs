using System;
using System.Linq;
using System.Threading.Tasks;
using IntelliFlo.Platform.Http.ExceptionHandling.Exceptions;
using IntelliFlo.Platform.NHibernate.Repositories;
using IntelliFlo.Platform.Transactions;
using IntelliFlo.Platform.v2.Security;
using Microservice.Platformer.Domain;
using Microservice.Platformer.v2.Contracts;
using Microservice.Platformer.v2.Mappers;
using Task = System.Threading.Tasks.Task;

namespace Microservice.Platformer.v2.Resources
{
    public class BulkImportResource : IBulkImportResource
    {
        private readonly IReadOnlyRepository<BulkImport> importRepository;
        private readonly ITenantSecurity tenantSecurity;
        private readonly IBulkImportMapper mapper;

        public BulkImportResource(IReadOnlyRepository<BulkImport> importRepository, ITenantSecurity tenantSecurity, IBulkImportMapper mapper)
        {
            if (importRepository == null)
                throw new ArgumentNullException(nameof(importRepository));
            if (tenantSecurity == null)
                throw new ArgumentNullException(nameof(tenantSecurity));
            if (mapper == null)
                throw new ArgumentNullException(nameof(mapper));

            this.importRepository = importRepository;
            this.tenantSecurity = tenantSecurity;
            this.mapper = mapper;
        }

        public async Task<BulkImportDocument> Get(Guid importId)
        {
            var import = importRepository
                .Query()
                .SingleOrDefault(r => r.Id == importId);

            if (import == null)
                throw new ResourceNotFoundException("bulkImport cannot be found");

            tenantSecurity.ForResource("BulkImport", importId).Assert(import.TenantId);

            var mapped = mapper.ToContaract(import);

            return mapped;
        }

        [Transaction]
        public Task<BulkImportDocumentCollection> GetAll(int top, int skip)
        {
            var query = importRepository
                .Query();

            var result = query
                .Skip(skip)
                .Take(top)
                .ToArray();

            if (result.Length == 0)
                return Task.FromResult(new BulkImportDocumentCollection());

            var count = query.Count();
            var mappedResult = result.Select(x => mapper.ToContaract(x)).ToArray();

            return Task.FromResult(new BulkImportDocumentCollection(mappedResult, count));
        }
    }
}