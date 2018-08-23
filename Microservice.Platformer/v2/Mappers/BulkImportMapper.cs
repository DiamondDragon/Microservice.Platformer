using System;
using Microservice.Platformer.Domain;
using Microservice.Platformer.v2.Contracts;

namespace Microservice.Platformer.v2.Mappers
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
