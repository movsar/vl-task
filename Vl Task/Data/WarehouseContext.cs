using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vl_Task.Models;

namespace Vl_Task.Data {
    public class WarehouseContext : DbContext {
        public DbSet<Product> Product { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductVersion> ProductVersion { get; set; }
        public DbSet<ProductVersion> ProductVersions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<ProductVersion>().ToTable("ProductVersion");
        }
        public WarehouseContext(DbContextOptions<WarehouseContext> options)
            : base(options) {
        }
    }
}
