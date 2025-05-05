using APICatalog.APICataolog.Data.Context;
using APICatalog.Data.Context;
using APICatalog.Data.Repositories.DAOs;
using APICatalog.Data.Repositories.Implementations;
using APICatalog.Data.Repositories.Interfaces;

namespace APICatalog.Core.DI
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Add your application services here
            services.AddDbContext<AppDbContext>();
            services.AddScoped<IDbTransaction, DbTransaction>();


            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>(); ;

            services.AddScoped<ProductDAO>();
            services.AddScoped<CategoryDAO>();
            return services;
        }
    }
}
