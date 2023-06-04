using Application.Interfaces;
using Infrastructure.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration, string wwwrootPath)
        {

            var connectionString = configuration.GetConnectionString("MariaDB");

            // DbContext
            services.AddDbContext<UpStorageShopDbContext>(opt => opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            services.AddScoped<IUpStorageShopDbContext>(provider => provider.GetRequiredService<UpStorageShopDbContext>());

            // Scoped Services
            //services.AddScoped<IExcelService, ExcelManager>();
            //services.AddScoped<IAuthenticationService, AuthenticationManager>();
            //services.AddSingleton<IJwtService, JwtManager>();

            // Singleton Services
            //services.AddSingleton<ITwoFactorService, TwoFactorManager>();
            //services.AddSingleton<IEmailService>(new EmailManager(wwwrootPath));


            return services;
        }
    }
}
