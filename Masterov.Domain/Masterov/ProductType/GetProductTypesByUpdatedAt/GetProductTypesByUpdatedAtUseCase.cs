using FluentValidation;
using Masterov.Domain.Masterov.ProductType.GetProductTypesByUpdatedAt.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductType.GetProductTypesByUpdatedAt;

public class GetProductTypesByUpdatedAtUseCase(IValidator<GetProductTypesByUpdatedAtQuery> validator,
    IGetProductTypesByUpdatedAtStorage storage) : IGetProductTypesByUpdatedAtUseCase
{
    public async Task<IEnumerable<ProductTypeDomain>?> Execute(GetProductTypesByUpdatedAtQuery query,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(query, cancellationToken);
        
        return await storage.GetProductTypesByUpdatedAt(query.UpdatedAt, cancellationToken);
    }
}