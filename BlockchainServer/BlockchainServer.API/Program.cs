
using BlockchainServer.Application.Services.Interfaces;
using BlockchainServer.Application.Services;
using BlockchainServer.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using BlockchainServer.Infrastructure.Services;

namespace BlockchainServer.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IBlockchainService, BlockchainService>();
            // var connectionString = builder.Configuration.GetConnectionString("AppConnection");
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
                builder.Configuration.GetConnectionString("AppConnection"),
                x => x.MigrationsAssembly("BlockchainServer.Infrastructure")
                ));


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
