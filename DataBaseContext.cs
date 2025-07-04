using Microsoft.EntityFrameworkCore;

namespace SearchEngine
{
    public class AccessDbContext : DbContext
    {
        public DbSet<Scanner> Scanner { get; set; } = null!;

        public DbSet<Creator> Creator { get; set; } = null!;

        public DbSet<Technology> Technology { get; set; } = null!;

        public DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder OptionsBuilder)
        {
            OptionsBuilder.UseJet(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=SearchEngine.accdb;");
        }
    }
}
