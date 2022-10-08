using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Vl_Task.Data;
namespace Vl_Task {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<WarehouseContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("WarehouseContext") ?? throw new InvalidOperationException("Connection string 'WarehouseContext' not found.")));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            // Add services to the container.
            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment()) {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();

            } else {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}