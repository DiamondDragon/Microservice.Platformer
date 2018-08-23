using System;
using IntelliFlo.Platform.NHibernate;

namespace Microservice.Platformer.Domain
{
    [Serializable]
    public class BulkImport : EqualityAndHashCodeProvider<BulkImport, Guid>
    {
        public virtual int TenantId { get; set; }
        public virtual int UserId { get; set; }
        public virtual DateTime EntryDate { get; set; }
        public virtual DateTime LastUpdatedDate { get; set; }
        public virtual string HeaderData { get; set; }
        public virtual bool? ShouldRecordsBeDuplicated { get; set; }
        public virtual string Status { get; set; }
        public virtual bool IsBeingProcessed { get; set; }
        public virtual int? NumberOfRecords { get; set; }
        public virtual int? NumberOfRecordsFailed { get; set; }
        public virtual int? NumberOfRecordsDuplicated { get; set; }
        public virtual string Message { get; set; }
        public virtual string SystemMessage { get; set; }
        public virtual string FileId { get; set; }
        public virtual string OriginalFileName { get; set; }
        public virtual string FileImportType { get; set; }
    }
}
