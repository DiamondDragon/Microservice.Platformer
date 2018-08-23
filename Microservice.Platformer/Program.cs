
namespace Monolith.Bulk
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
