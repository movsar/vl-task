﻿using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data {
    public static class ServiceCollectionExtensions {

        public static void RegisterServices(this IServiceCollection services) {
            var connectionString = "Data Source=DESKTOP-BODQELM;Database=TestDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            services.AddDbContext<WarehouseContext>(options => options.UseSqlServer(connectionString ?? throw new InvalidOperationException("Connection string 'WarehouseContext' not found.")));
            services.AddScoped<ProductRepository>();
            services.AddScoped<ProductVersionsRepository>();
            services.AddScoped<Storage>();
        }
    }
}