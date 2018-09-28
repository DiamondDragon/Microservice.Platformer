using IntelliFlo.Platform.Database;
using IntelliFlo.Platform.Database.Impl.Initialisers;
using IntelliFlo.Platform.NHibernate;
using Microsoft.Extensions.Logging;

namespace Microservice.Platformer.Host.DbInitialization
{
    [DbProfile("subsys")]
    public class SubSystemTestingInitializer : InitialiserBase
    {
        public SubSystemTestingInitializer(
            ILoggerFactory loggerFactory,
            IReadWriteSessionFactoryProvider readWriteSessionFactoryProvider,
            IDatabaseInstance instance)
            : base(loggerFactory.CreateLogger<InitialiserBase>(), new AddSubsysTestData(loggerFactory, readWriteSessionFactoryProvider))
        {
        }
    }
}
