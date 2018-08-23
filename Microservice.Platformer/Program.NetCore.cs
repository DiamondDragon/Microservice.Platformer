using IntelliFlo;
using Microsoft.AspNetCore.Hosting;
using Microservice.Platformer.Properties;

namespace Microservice.Platformer
{
    partial class Program
    {
#if !NET471

        public static void RunUsingNetCore(string[] args)
        {
            MicroserviceHost
                .Build<Startup>(Settings.Default.BaseAddress, args)
                .Run();
        }

#endif
    }
}