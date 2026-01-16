using FluentValidation;
using Masterov.Domain.Masterov.ProductType.GetProductTypesByCreatedAt.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductType.GetProductTypesByCreatedAt;

public class GetProductTypesByCreatedAtUseCase(IValidator<GetProductTypesByCreatedAtQuery> validator,
    IGetProductTypesByCreatedAtStorage storage) 
    : IGetProductTypesByCreatedAtUseCase
{
    public async Task<IEnumerable<ProductTypeDomain>?> Execute(GetProductTypesByCreatedAtQuery query,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(query, cancellationToken);
        return await storage.GetProductTypesByCreatedAt(query.CreatedAt, cancellationToken);
    }
}