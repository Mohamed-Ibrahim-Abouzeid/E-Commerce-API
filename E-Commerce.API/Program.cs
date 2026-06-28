
using E_Commerce.API.Extensions;
using E_Commerce.Application;
using E_Commerce.Infrastructure;
using Microsoft.Extensions.FileProviders;

namespace E_Commerce.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddInfrastructeServices(builder.Configuration);
            builder.Services.AddApplicationServices();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();
            app.SeedDataBaseAsync();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }
            app.UseStaticFiles(new StaticFileOptions { 
            FileProvider=new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath,"Files")),
            RequestPath="/Files"
            });
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
