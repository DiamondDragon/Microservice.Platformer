
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
