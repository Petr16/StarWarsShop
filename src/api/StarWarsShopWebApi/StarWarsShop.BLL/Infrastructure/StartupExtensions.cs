using AutoMapper;
using StarWarsShop.BLL.Services;
using StarWarsShop.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace StarWarsShop.BLL.Infrastructure
{
    public static class StartupExtensions
    {
        public static void AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));
        }

        public static void AddStarWarsShopDBContext(this IServiceCollection services, string connectionString)
        {
            //services.AddDbContext<CustomerManagerDbContext>(options => options.UseNpgsql(connectionString));
            services.AddDbContext<StarWarsShopDBContext>();
        }

        public static void AddStarWarsShopBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<RequestService>();
            //services.AddScoped<CustomerService>();
            //services.AddScoped<StatusRequestService>();
        }
    }
}