using Business.Abstracts;
using Business.Concretes;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.JWT;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

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
            services.AddScoped<IInterpreterService, InterpreterManager>();
            services.AddScoped<IBookService, BookManager>();
            services.AddScoped<IDepositBookService, DepositBookManager>();
            services.AddScoped<IUserService, UserManager>();
            services.AddScoped<IAuthService,AuthManager>();
            services.AddScoped<ITokenHelper,JwtHelper>();

           


            return services;
        }
    }
}
