using Masterov.Domain.Masterov.FinishedProduct.AddFinishedProduct;
using Masterov.Domain.Masterov.FinishedProduct.DeleteFinishedProduct;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductById;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductByName;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductOrders;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProducts;
using Masterov.Domain.Masterov.FinishedProduct.UpdateFinishedProduct;
using Masterov.Domain.Masterov.ProductionOrder.GetProductionOrderById;
using Masterov.Domain.Masterov.ProductionOrder.GetProductionOrders;
using Masterov.Domain.Masterov.ProductionOrder.GetProductionOrdersByCompletedAt;
using Masterov.Domain.Masterov.ProductionOrder.GetProductionOrdersByCreatedAt;
using Masterov.Domain.Masterov.ProductionOrder.GetProductionOrdersByDescription;
using Masterov.Domain.Masterov.ProductType.AddProductType;
using Masterov.Domain.Masterov.ProductType.DeleteProductType;
using Masterov.Domain.Masterov.ProductType.GetProductsType;
using Masterov.Domain.Masterov.ProductType.GetProductTypeById;
using Masterov.Domain.Masterov.ProductType.GetProductTypeByName;
using Masterov.Domain.Masterov.ProductType.UpdateProductType;
using Microsoft.Extensions.DependencyInjection;

namespace Masterov.Domain.DI;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services
            .AddScoped<IGetFinishedProductByIdUseCase, GetFinishedProductByIdUseCase>()
            .AddScoped<IGetFinishedProductByNameUseCase, GetFinishedProductByNameUseCase>()
            .AddScoped<IDeleteFinishedProductUseCase, DeleteFinishedProductUseCase>()
            .AddScoped<IAddFinishedProductUseCase, AddFinishedProductUseCase>()
            .AddScoped<IUpdateFinishedProductUseCase, UpdateFinishedProductUseCase>()
            .AddScoped<IGetFinishedProductOrdersUseCase, GetFinishedProductOrdersUseCase>()
            .AddScoped<IGetFinishedProductsUseCase, GetFinishedProductsUseCase>();

        services
            .AddScoped<IGetProductionOrdersUseCase, GetProductionOrdersUseCase>()
            .AddScoped<IGetProductionOrdersByCreatedAtUseCase, GetProductionOrdersByCreatedAtUseCase>()
            .AddScoped<IGetProductionOrdersByCompletedAtUseCase, GetProductionOrdersByCompletedAtUseCase>()
            .AddScoped<IGetProductionOrdersByDescriptionUseCase, GetProductionOrdersByDescriptionUseCase>()
            .AddScoped<IGetProductionOrderByIdUseCase, GetProductionOrderByIdUseCase>();
        
        services
            .AddScoped<IUpdateProductTypeUseCase, UpdateProductTypeUseCase>()
            .AddScoped<IDeleteProductTypeUseCase, DeleteProductTypeUseCase>()
            .AddScoped<IGetProductTypeByNameUseCase, GetProductTypeByNameUseCase>()
            .AddScoped<IAddProductTypeUseCase, AddProductTypeUseCase>()
            .AddScoped<IGetProductTypeByIdUseCase, GetProductTypeByIdUseCase>()
            .AddScoped<IGetProductsTypeUseCase, GetProductsTypeUseCase>();
            
        return services;
    }
}