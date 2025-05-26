using BlockchainServer.Application.Services;
using BlockchainServer.Application.Services.Interfaces;
using BlockchainServer.Infrastructure.DatabaseContext;
using BlockchainServer.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;


namespace BlockchainWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
                builder.Configuration.GetConnectionString("AppConnection"),
                x => x.MigrationsAssembly("BlockchainServer.Infrastructure")
                ));

            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IBlockchainService, BlockchainService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
