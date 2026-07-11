using E_Commerce.Application.Contracts;
using E_Commerce.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application
{
    public static class ApplicationSeervicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services) {
            services.AddAutoMapper(typeof(ApplicationSeervicesRegistration).Assembly);
            services.AddScoped<IProductService, ProductService>();
           services.AddScoped<IBasketService, BasketService>();
           services.AddSingleton<ICasheService,CasheService>();
            return services;
        }
    }
}
