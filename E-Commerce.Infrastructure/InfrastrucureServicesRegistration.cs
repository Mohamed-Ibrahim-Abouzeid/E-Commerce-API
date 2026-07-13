using E_Commerce.Application.Contracts;
using E_Commerce.Domain.Contracts;
using E_Commerce.Infrastructure.Data;
using E_Commerce.Infrastructure.Identity.Data;
using E_Commerce.Infrastructure.Identity.Entities;
using E_Commerce.Infrastructure.Identity.Services;
using E_Commerce.Infrastructure.Repositories;
using E_Commerce.Infrastructure.Seeding;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Infrastructure
{
    public static class InfrastrucureServicesRegistration
    {
        public static IServiceCollection AddInfrastructeServices(this IServiceCollection services, IConfiguration configuration) {
            services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            }
                );
            services.AddKeyedScoped<IDataSeeder, CatalogDataSeeder>("Catalog");
            services.AddKeyedScoped<IDataSeeder, IdentityDataSeeder>("Identity");
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IConnectionMultiplexer>(Config =>
            {
                return ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnection"));
            });
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddSingleton<ICasheRepository, CasheRepository>();
           services.AddDbContext<StoreIdentityDbContext>(options=>
           {
               options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));
           });
          
            services.AddIdentityCore<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<StoreIdentityDbContext>();
            services.AddScoped<IIdentityService, IdentityService>();
            var jwtSettings = configuration.GetSection("JwtSettings")
            ?? throw new InvalidOperationException("JwtSettings section is missing in the configuration.");
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme= JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    RequireExpirationTime = true,
                    RequireSignedTokens = true,
                    IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtSettings["Secret"] ?? throw new InvalidOperationException("JwtSettings:Secret is missing in the configuration.")))
                };
            });
            return services;
        }
    }
}
