using IntelliFlo.Platform.Database;
using IntelliFlo.Platform.Database.Impl.Initialisers;
using IntelliFlo.Platform.NHibernate;

namespace Microservice.Platformer.Host.DbInitialization
{
    [DbProfile("subsys")]
    public class SubSystemTestingInitializer : InitialiserBase
    {
        public SubSystemTestingInitializer(
            IReadWriteSessionFactoryProvider readWriteSessionFactoryProvider,
            IDatabaseInstance instance)
            : base(new AddSubsysTestData(readWriteSessionFactoryProvider))
        {
        }
    }
}
