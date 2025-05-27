using Masterov.Domain.Masterov.Product.GetProductById;
using Masterov.Domain.Masterov.Product.GetProducts;
using Masterov.Domain.Masterov.ProductType.GetProductsType;
using Microsoft.Extensions.DependencyInjection;

namespace Masterov.Domain.DI;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services
            .AddScoped<IGetProductByIdUseCase, GetProductByIdUseCase>()
            .AddScoped<IGetProductsUseCase, GetProductsUseCase>();

        services
            .AddScoped<IGetProductsTypeUseCase, GetProductsTypeUseCase>();
            
        return services;
    }
}