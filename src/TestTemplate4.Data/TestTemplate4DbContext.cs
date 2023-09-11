using MassTransit;
using Microsoft.EntityFrameworkCore;
using TestTemplate4.Core.Entities;

namespace TestTemplate4.Data
{
    public class TestTemplate4DbContext : DbContext
    {
        public TestTemplate4DbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Foo> Foos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddInboxStateEntity();
            modelBuilder.AddOutboxMessageEntity();
            modelBuilder.AddOutboxStateEntity();
        }
    }
}
