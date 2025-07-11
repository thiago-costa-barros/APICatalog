﻿using APICatalog.APICataolog.Data.Context;
using APICatalog.Core.Services;
using APICatalog.Core.Services.Helpers;
using APICatalog.Core.Services.Interfaces;
using APICatalog.Data.Context;
using APICatalog.Data.Repositories;
using APICatalog.Data.Repositories.DAOs;
using APICatalog.Data.Repositories.Interfaces;

namespace APICatalog.Core.DI
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddDependencyInjectionConfig(this IServiceCollection services)
        {
            // Infrastructure
            services.AddDbContext<AppDbContext>();

            // Unit of Work
            services.AddScoped<IDbTransaction, DbTransaction>();

            //Services
            services.AddServices();

            //Repositories
            services.AddRepositories();

            //Data Object Access
            services.AddDataObjectAccess();

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            // Add your application services here
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAuthServiceHelper, AuthServiceHelper>();

            return services;
        }
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            // Add your application repositories here
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();

            return services;
        }
        public static IServiceCollection AddDataObjectAccess(this IServiceCollection services)
        {
            // Add your DAOs here
            services.AddScoped<CategoryDAO>();
            services.AddScoped<ProductDAO>();
            services.AddScoped<UserDAO>();
            services.AddScoped<AuthDAO>();

            return services;
        }
    }
}
