using IntelliFlo;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.WindowsServices;


namespace Microservice.Platformer
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = MicroserviceHost
                .Build<Startup>(args);

            if (MicroserviceHost.ShouldRunAsService(args))
                host.RunAsService();
            else
                host.Run();
        }
    }
}
