using Masterov.Domain.Masterov.GetProducts;
using Microsoft.Extensions.DependencyInjection;

namespace Masterov.Domain.DI;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services
            .AddScoped<IGetProductsUseCase, GetProductsUseCase>();
        return services;
    }
}