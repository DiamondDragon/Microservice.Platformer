using Microservice.Platformer.Domain;
using Microservice.Platformer.v2.Contracts;

namespace Microservice.Platformer.v2.Mappers
{
    public interface IBulkImportMapper
    {
        BulkImportDocument ToContaract(BulkImport bulkImport);
    }
}