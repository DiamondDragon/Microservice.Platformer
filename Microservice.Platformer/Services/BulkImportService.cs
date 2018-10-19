using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using IntelliFlo.Platform.EntityFramework.Repositories;
using IntelliFlo.Platform.EntityFramework.Specifications;
using IntelliFlo.Platform.Transactions;
using Microservice.Platformer.DataLayer;
using Microservice.Platformer.Domain;

namespace Microservice.Platformer.Services
{
    public class BulkImportService : IBulkImportService
    {
        private readonly IRepository<NewBulkImport> repository;

        public BulkImportService(IRepository<NewBulkImport> repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [Transaction]
        public void GetData()
        {
            var result = repository.ListAsync(null, null, new Specification()).Result;
        }

        [Transaction]
        public void AddData()
        {
            var bulk = new NewBulkImport
            {
                Name = "Some data",
                CreatedAt = DateTime.UtcNow,
            };


            repository.AddAsync(bulk).Wait();
        }

        public class Specification : ISpecification<NewBulkImport>
        {
            public IEnumerable<Expression<Func<NewBulkImport, object>>> Includes { get; }
            public IQueryable<NewBulkImport> AddPredicates(IQueryable<NewBulkImport> query)
            {
                return query;
            }

            public IQueryable<NewBulkImport> AddSorting(IQueryable<NewBulkImport> query)
            {
                return query;
            }
        }

        //private readonly IRepository<BulkImport> repository;

        //public BulkImportService(IRepository<BulkImport> repository)
        //{
        //    this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        //}

        //[Transaction]
        //public void GetData()
        //{
        //    var result = repository.FindFirst();
        //}

        //[Transaction]
        //public void AddData()
        //{
        //    var bulk = new BulkImport
        //    {
        //        HeaderData = "Some data",
        //        EntryDate = DateTime.UtcNow,
        //        LastUpdatedDate = DateTime.UtcNow,
        //    };


        //    repository.Save(bulk);
        //}
    }
}
