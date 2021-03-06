using System;
using System.Data;
using System.Linq;
using IntelliFlo.Platform.Database;
using IntelliFlo.Platform.NHibernate;
using Microservice.Platformer.Domain;
using Microsoft.Extensions.Logging;
using NHibernate;
using ILoggerFactory = Microsoft.Extensions.Logging.ILoggerFactory;

namespace Microservice.Platformer.Host.DbInitialization
{
    public class AddSubsysTestData : TaskBase<AddSubsysTestData>
    {
        private readonly IReadWriteSessionFactoryProvider provider;

        public AddSubsysTestData(ILoggerFactory loggerFactory, IReadWriteSessionFactoryProvider provider)
            : base(loggerFactory)
        {
            this.provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        private void WithTransaction(ISession session, Action action)
        {
            using (var tx = session.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                action();
                tx.Commit();
            }
        }

        public override object Execute(IDatabaseSettings settings)
        {

            Logger.LogInformation("Action=AddSubsysTestData, Message=About to add subsys test data");
            using (var session = provider.SessionFactory.OpenSession())
            {
                WithTransaction(session, () => CleanUpData(session));
                WithTransaction(session, () => AddRefData(session));
                WithTransaction(session, () => AddOtherData(session));

                return true;
            }
        }

        private void CleanUpData(ISession session)
        {
            Logger.LogInformation("Action=AddSubsysTestData, Message=Cleaning up");
        }

        private void AddOtherData(ISession session)
        {
            Logger.LogInformation("Action=AddSubsysTestData, Message=Adding data");
            AddImportIntelliFlo(session);
        }

        private void AddImportIntelliFlo(ISession session)
        {
            const string importId = "3EB27ADC-9404-4451-BD70-F3E091F36339";

            var import = new BulkImport
            {
                Id = 1234,
                EntryDate = new DateTime(2002, 01, 01, 0, 0, 0, DateTimeKind.Utc),
                LastUpdatedDate = new DateTime(2002, 01, 01, 0, 0, 0, DateTimeKind.Utc),
            };

            var query = session.Query<BulkImport>().Where(x => x.Id == 12345);

            if (query.Any())
                return;

            session.Save(import);
        }

        private void AddRefData(ISession session)
        {
            Logger.LogInformation("Action=AddSubsysTestData, Message=Adding ref data");
        }

        public override void Dispose()
        {
        }
    }
}
