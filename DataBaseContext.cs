using Microsoft.EntityFrameworkCore;

namespace SearchEngine
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Scanner> Scanners { get; set; } = null!;

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Scanners.db");
            optionsBuilder.EnableSensitiveDataLogging(); // Для отладки
        }
    }
}
