using System.Reflection;
using Masterov.Domain.Masterov.FinishedProduct.AddFinishedProduct;
using Masterov.Domain.Masterov.FinishedProduct.DeleteFinishedProduct;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductById;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductByName;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductOrders;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProducts;
using Masterov.Domain.Masterov.FinishedProduct.UpdateFinishedProduct;
using Masterov.Domain.Masterov.ProductionOrder.GetProductionOrderById;
using Masterov.Domain.Masterov.ProductionOrder.GetProductionOrders;
using Masterov.Domain.Masterov.ProductType.AddProductType;
using Masterov.Domain.Masterov.ProductType.DeleteProductType;
using Masterov.Domain.Masterov.ProductType.GetProductsType;
using Masterov.Domain.Masterov.ProductType.GetProductTypeById;
using Masterov.Domain.Masterov.ProductType.GetProductTypeByName;
using Masterov.Domain.Masterov.ProductType.UpdateProductType;
using Masterov.Storage.Extension;
using Masterov.Storage.Storages.Masterov;
using Masterov.Storage.Storages.Masterov.FinishedProduct;
using Masterov.Storage.Storages.Masterov.ProductionOrder;
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
            .AddScoped<IGetFinishedProductByNameStorage, GetFinishedProductByNameStorage>()
            .AddScoped<IAddFinishedProductStorage, AddFinishedProductStorage>()
            .AddScoped<IGetFinishedProductByIdStorage, GetFinishedProductByIdStorage>()
            .AddScoped<IDeleteFinishedProductStorage, DeleteFinishedProductStorage>()
            .AddScoped<IUpdateFinishedProductStorage, UpdateFinishedProductStorage>()
            .AddScoped<IGetFinishedProductOrdersStorage, GetFinishedProductOrdersStorage>()
            .AddScoped<IGuidFactory, GuidFactory>()
            .AddDbContextPool<MasterovDbContext>(options => options.UseMySql(dbConnectionString, ServerVersion.AutoDetect(dbConnectionString)));

        services
            .AddScoped<IGetProductionOrdersStorage, GetProductionOrdersStorage>()
            .AddScoped<IGetProductionOrderByIdStorage, GetProductionOrderByIdStorage>();
        
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