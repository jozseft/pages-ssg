using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PagesData.Entities;

namespace PagesData.Context
{
    public class PagesContext : IdentityDbContext<User, UserRole, Guid>
    {
        public DbSet<Post> Posts { get; set; }

        public PagesContext(DbContextOptions<PagesContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Post>(entity =>
            {
                entity.HasIndex(e => e.SourceName).IsUnique();
            });
        }
    }
}
