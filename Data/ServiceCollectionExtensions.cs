using Data.Repositories;
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
            services.AddDbContext<WarehouseContext>(options => options.UseSqlServer(WarehouseContext.GetCurrentConnectionString()));
            services.AddScoped<ProductRepository>();
            services.AddScoped<ProductVersionsRepository>();
            services.AddScoped<Storage>();
        }
    }
}
