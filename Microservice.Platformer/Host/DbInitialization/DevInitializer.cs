using IntelliFlo.Platform.Database;
using IntelliFlo.Platform.Database.Impl.Initialisers;
using Microsoft.Extensions.Logging;

namespace Microservice.Platformer.Host.DbInitialization
{
    [DbProfile("dev")]
    public class DevInitializer : InitialiserBase
    {
        public DevInitializer(ILoggerFactory loggerFactory)
            : base(loggerFactory.CreateLogger<InitialiserBase>())
        {
        }
    }
}
