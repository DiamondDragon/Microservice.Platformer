using System;
using System.Data;
using System.Linq;
using System.Reflection;
using IntelliFlo.Platform.Database;
using IntelliFlo.Platform.NHibernate;
using log4net;
using Monolith.Bulk.Domain;
using NHibernate;

namespace Monolith.Bulk.Host.DbInitialization
{
    public class AddSubsysTestData : TaskBase<AddSubsysTestData>
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IReadWriteSessionFactoryProvider provider;

        public AddSubsysTestData(IReadWriteSessionFactoryProvider provider)
        {
            if (provider == null)
                throw new ArgumentNullException("provider");

            this.provider = provider;
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
            log.InfoFormat("Action=AddSubsysTestData, Message=About to add subsys test data");
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
            log.InfoFormat("Action=AddSubsysTestData, Message=Cleaning up");
        }

        private void AddOtherData(ISession session)
        {
            log.InfoFormat("Action=AddSubsysTestData, Message=Adding data");
            AddImportIntelliFlo(session);
        }

        private void AddImportIntelliFlo(ISession session)
        {
            const string importId = "3EB27ADC-9404-4451-BD70-F3E091F36339";

            var import = new BulkImport
            {
                Id = Guid.Parse(importId),
                TenantId = 10155,
                EntryDate = new DateTime(2002, 01, 01, 0, 0, 0, DateTimeKind.Utc),
                LastUpdatedDate = new DateTime(2002, 01, 01, 0, 0, 0, DateTimeKind.Utc),
                Status = "AwaitingProcessing"
            };

            var query = session.Query<BulkImport>().Where(x => x.Id == Guid.Parse(importId));

            if (query.Any())
                return;

            session.Save(import);
        }

        private void AddRefData(ISession session)
        {
            log.InfoFormat("Action=AddSubsysTestData, Message=Adding ref data");
        }

        public override void Dispose()
        {
        }
    }
}
