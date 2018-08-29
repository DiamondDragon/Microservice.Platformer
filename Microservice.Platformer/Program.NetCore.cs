using IntelliFlo;
using Microsoft.AspNetCore.Hosting;

namespace Microservice.Platformer
{
    partial class Program
    {
#if !NET471

        public static void RunUsingNetCore(string[] args)
        {
            MicroserviceHost
                .Build<Startup>(args)
                .Run();
        }

#endif
    }
}
