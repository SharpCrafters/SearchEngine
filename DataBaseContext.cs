using Microsoft.EntityFrameworkCore;

namespace SearchEngine
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Scanner> Scanners { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Scanners.db");
        }
    }
}
