﻿using Masterov.Domain.Masterov.FinishedProduct.AddFinishedProduct;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductById;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductByName;
using Masterov.Domain.Masterov.FinishedProduct.GetProducts;
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
            .AddScoped<IAddFinishedProductUseCase, AddFinishedProductUseCase>()
            .AddScoped<IGetFinishedProductsUseCase, GetFinishedProductsUseCase>();

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