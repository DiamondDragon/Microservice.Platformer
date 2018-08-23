using System;
using Monolith.Bulk.Domain;
using Monolith.Bulk.v2.Contracts;

namespace Monolith.Bulk.v2.Mappers
{
    public class BulkImportMapper: IBulkImportMapper
    {
        public BulkImportDocument ToContaract(BulkImport bulkImport)
        {
            if (bulkImport == null)
                throw new ArgumentNullException(nameof(bulkImport));

            return new BulkImportDocument
            {
                Id = bulkImport.Id,
                Message = bulkImport.Message,
                RecordCount = bulkImport.NumberOfRecords ?? 0,
                Status = bulkImport.Status,
                UploadedAt = bulkImport.EntryDate
            };
        }
    }
}
