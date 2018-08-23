
using System.IO;
using System.Linq;
using IntelliFlo.AppStartup.Utils;


namespace Microservice.Platformer
{
    partial class Program
    {
        static void Main(string[] args)
        {
#if NET471

            RunUsingNetFramework(args);
 
#else

            RunUsingNetCore(args);

#endif
        }
    }
}
