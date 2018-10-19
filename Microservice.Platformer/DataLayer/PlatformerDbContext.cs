using Microsoft.EntityFrameworkCore;

namespace Microservice.Platformer.DataLayer
{
    public class PlatformerDbContext : DbContext
    {
        public DbSet<NewBulkImport> NewBulkImports { get; set; }

        public PlatformerDbContext(DbContextOptions<PlatformerDbContext> options)
            : base(options)
        {
        }
    }
}
