using Microsoft.Extensions.DependencyInjection;
using ProductApplication.Application.Services.Categories;
using ProductApplication.Application.Services.Suppliers;
using ProductApplication.Domain.Interfaces;
using ProductApplication.Infra.Repository;

namespace ProductApplication.Web.DependencyInjection
{
    public static class SetupDependencies
    {

        public static void SetupServicesDependencies(this IServiceCollection services)
        {
            services.AddScoped<ISupplierService, SupplierService>();
            services.AddScoped<ICategoryService, CategoryService>();
        }

        public static void SetupRepositoriesDependencies(this IServiceCollection services)
        {
            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
        }

    }
}
