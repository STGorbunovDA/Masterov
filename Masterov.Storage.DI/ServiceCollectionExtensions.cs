using System.Reflection;
using Masterov.Domain.Masterov.Product.GetProductById;
using Masterov.Domain.Masterov.Product.GetProducts;
using Masterov.Storage.Extension;
using Masterov.Storage.Storages.Masterov;
using Masterov.Storage.Storages.Masterov.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Masterov.Storage.DI;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddStorage(this IServiceCollection services, string? dbConnectionString)
    {
        services
            .AddScoped<IGetProductsStorage, GetProductsStorage>()
            .AddScoped<IGetProductByIdStorage, GetProductByIdStorage>()
            .AddScoped<IGuidFactory, GuidFactory>()
            .AddDbContextPool<MasterovDbContext>(options => options.UseMySql(dbConnectionString, ServerVersion.AutoDetect(dbConnectionString)));
        
        
        services.AddMemoryCache();
        
        services.AddAutoMapper(config => config
            .AddMaps(Assembly.GetAssembly(typeof(MasterovDbContext))));
        
        return services;
    }
}