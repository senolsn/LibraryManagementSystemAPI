using Business.Abstracts;
using Business.Concretes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public static class BusinessServiceRegistration
    {
        public static IServiceCollection AddBusinessServiceRegistration(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IAuthorService, AuthorManager>();
            services.AddScoped<IDepartmentService, DepartmentManager>();
            services.AddScoped<ILocationService, LocationManager>();
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<IFacultyService, FacultyManager>();
            services.AddScoped<IPublisherService, PublisherManager>();
            services.AddScoped<ILanguageService, LanguageManager>();

            return services;
        }
    }
}
