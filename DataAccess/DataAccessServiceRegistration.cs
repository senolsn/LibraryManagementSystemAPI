using DataAccess.Abstracts;
using DataAccess.Concretes;
using DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

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
            services.AddScoped<IAuthorDal, EfAuthorDal>();
            services.AddScoped<IDepartmentDal, EfDepartmentDal>();
            services.AddScoped<IFacultyDal, EfFacultyDal>();
            services.AddScoped<ICategoryDal, EfCategoryDal>();
            services.AddScoped<IPublisherDal, EfPublisherDal>();
            services.AddScoped<ILocationDal, EfLocationDal>();
            services.AddScoped<IBookDal, EfBookDal>();
            services.AddScoped<IDepositBookDal, EfDepositBookDal>();
            services.AddScoped<IUserDal, EfUserDal>();
            services.AddScoped<IBookAuthorDal, EfBookAuthorDal>();
            services.AddScoped<IBookCategoryDal, EfBookCategoryDal>();

            return services;

        }
    }
}
