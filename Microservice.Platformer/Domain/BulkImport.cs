using System;
using IntelliFlo.Platform.NHibernate;

namespace Microservice.Platformer.Domain
{
    [Serializable]
    public class BulkImport : EqualityAndHashCodeProvider<BulkImport, int>
    {
        public virtual DateTime EntryDate { get; set; }
        public virtual DateTime LastUpdatedDate { get; set; }
        public virtual string HeaderData { get; set; }
    }
}
