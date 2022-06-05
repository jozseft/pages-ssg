using Microsoft.EntityFrameworkCore;
using PagesData.Entities;

namespace PagesData.Context
{
    public class PagesContext : DbContext
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
            builder.Entity<Post>(entity => {
                entity.HasIndex(e => e.SourceName).IsUnique();
            });
        }
    }
}
