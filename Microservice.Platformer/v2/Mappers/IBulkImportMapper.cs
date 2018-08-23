using Monolith.Bulk.Domain;
using Monolith.Bulk.v2.Contracts;

namespace Monolith.Bulk.v2.Mappers
{
    public interface IBulkImportMapper
    {
        BulkImportDocument ToContaract(BulkImport bulkImport);
    }
}