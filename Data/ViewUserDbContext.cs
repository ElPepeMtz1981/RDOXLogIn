using Microsoft.EntityFrameworkCore;

namespace RDOXMES.Login
{
    public class ViewUserDbContext : DbContext
    {
        public DbSet<ViewUserRol> Users => Set<ViewUserRol>();

        public ViewUserDbContext(DbContextOptions<ViewUserDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Usuario de prueba
            modelBuilder.Entity<ViewUserRol>().ToView("ViewUserRol");
        }
    }
}