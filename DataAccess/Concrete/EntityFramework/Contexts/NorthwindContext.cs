using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework.Contexts
{
    public class NorthwindContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=Northwind;Trusted_Connection=True");
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().Property(p => p.Id).HasColumnName("ProductID");
            modelBuilder.Entity<Product>().Property(p => p.Name).HasColumnName("ProductName");
            modelBuilder.Entity<Product>().Property(p => p.CategoryId).HasColumnName("CategoryID");
            modelBuilder.Entity<Category>().Property(c => c.Id).HasColumnName("CategoryID");
            modelBuilder.Entity<Category>().Property(c => c.Name).HasColumnName("CategoryName");

        }
    }
}
