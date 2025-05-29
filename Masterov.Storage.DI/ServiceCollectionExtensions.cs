using System.Reflection;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductById;
using Masterov.Domain.Masterov.FinishedProduct.GetProducts;
using Masterov.Domain.Masterov.ProductType.AddProductType;
using Masterov.Domain.Masterov.ProductType.DeleteProductType;
using Masterov.Domain.Masterov.ProductType.GetProductsType;
using Masterov.Domain.Masterov.ProductType.GetProductTypeById;
using Masterov.Domain.Masterov.ProductType.GetProductTypeByName;
using Masterov.Domain.Masterov.ProductType.UpdateProductType;
using Masterov.Storage.Extension;
using Masterov.Storage.Storages.Masterov;
using Masterov.Storage.Storages.Masterov.FinishedProduct;
using Masterov.Storage.Storages.Masterov.ProductType;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Masterov.Storage.DI;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddStorage(this IServiceCollection services, string? dbConnectionString)
    {
        services
            .AddScoped<IGetFinishedProductsStorage, GetFinishedProductsStorage>()
            .AddScoped<IGetFinishedProductByIdStorage, GetFinishedProductByIdStorage>()
            .AddScoped<IGuidFactory, GuidFactory>()
            .AddDbContextPool<MasterovDbContext>(options => options.UseMySql(dbConnectionString, ServerVersion.AutoDetect(dbConnectionString)));

        services
            .AddScoped<IUpdateProductTypeStorage, UpdateProductTypeStorage>()
            .AddScoped<IDeleteProductTypeStorage, DeleteProductTypeStorage>()
            .AddScoped<IAddProductTypeStorage, AddProductTypeStorage>()
            .AddScoped<IGetProductTypeByNameStorage, GetProductTypeByNameStorage>()
            .AddScoped<IGetProductsTypeStorage, GetProductsTypeStorage>()
            .AddScoped<IGetProductTypeByIdStorage, GetProductTypeByIdStorage>();
        
        
        services.AddMemoryCache();
        
        services.AddAutoMapper(config => config
            .AddMaps(Assembly.GetAssembly(typeof(MasterovDbContext))));
        
        return services;
    }
}