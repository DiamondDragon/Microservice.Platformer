using IntelliFlo.Platform.Database;
using IntelliFlo.Platform.Database.Impl.Initialisers;

namespace Monolith.Bulk.Host.DbInitialization
{
    [DbProfile("dev")]
    public class DevInitializer : InitialiserBase
    {
        public DevInitializer() : base() { }
    }
}
