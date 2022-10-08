﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data {
    public class WarehouseContext : DbContext {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductVersion> ProductVersions { get; set; }
        public DbSet<ProductVersionSearchResult> ProductVersionSearchResult { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<ProductVersion>().ToTable("ProductVersion");
            modelBuilder.Entity<ProductVersionSearchResult>().HasNoKey();
        }
        public WarehouseContext(DbContextOptions<WarehouseContext> options): base(options) {}

    }
}
