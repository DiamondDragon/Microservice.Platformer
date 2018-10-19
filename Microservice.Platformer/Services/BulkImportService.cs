using System;
using System.Collections.Generic;
using System.Text;
using IntelliFlo.Platform.NHibernate.Repositories;
using IntelliFlo.Platform.Transactions;
using Microservice.Platformer.Domain;
using NHibernate.Criterion;

namespace Microservice.Platformer.Services
{
    public class BulkImportService : IBulkImportService
    {
        private readonly IRepository<BulkImport> repository;

        public BulkImportService(IRepository<BulkImport> repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [Transaction]
        public void GetData()
        {
            var result = repository.FindFirst();
        }

        [Transaction]
        public void AddData()
        {
            var bulk = new BulkImport
            {
                HeaderData = "Some data",
                EntryDate = DateTime.UtcNow,
                LastUpdatedDate = DateTime.UtcNow,
            };


            repository.Save(bulk);
        }
    }
}
