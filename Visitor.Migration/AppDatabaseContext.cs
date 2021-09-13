using System;
using Microsoft.EntityFrameworkCore;
using Visitor.Domain.Model;

namespace Visitor.DatabaseMigration
{
    public class AppDatabaseContext : DbContext
    {
        public AppDatabaseContext(DbContextOptions<AppDatabaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Visitor.Domain.Model.Visitor> Visitors { get; set; }
    }
}
