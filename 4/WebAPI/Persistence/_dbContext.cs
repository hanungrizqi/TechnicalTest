using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Persistence
{
    public partial class _dbContext : DbContext
    {
        public _dbContext(DbContextOptions<_dbContext> options) : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(a =>
            {
                a.HasKey(a => a.CustomerId);
                a.ToTable("Customer");

                a.Property(a => a.CreatedBy).HasColumnName("CreatedBy");
            });
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
