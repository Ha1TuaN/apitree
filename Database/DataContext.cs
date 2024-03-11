using Microsoft.EntityFrameworkCore;

namespace apitree.Database
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options) { }
        public DbSet<Tree> Tree { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tree>().ToTable(nameof(Tree)).HasKey(x => x.Id);
        }
    }
}
