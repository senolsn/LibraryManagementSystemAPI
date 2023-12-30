using DataAccess.Abstracts;
using DataAccess.Concretes;
using DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public static class DataAccessServiceRegistration
    {

        public static IServiceCollection AddDataAccessServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<LibraryAPIDbContext>(
            options => options.UseMySql(
                configuration.GetConnectionString("MySQLConnectionString"),
                new MySqlServerVersion(new Version(10, 3, 39))
                )
            );

            services.AddScoped<ILanguageDal, EfLanguageDal>();
            

            return services;

        }
    }
}
