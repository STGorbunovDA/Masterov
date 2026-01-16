using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.ProductType.GetProductTypesByName.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductType.GetProductTypesByName;

public class GetProductTypesByNameUseCase(IValidator<GetProductTypesByNameQuery> validator, IGetProductTypesByNameStorage storage) : IGetProductTypesByNameUseCase
{
    public async Task<IEnumerable<ProductTypeDomain?>> Execute(GetProductTypesByNameQuery productTypesByNameQuery, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(productTypesByNameQuery, cancellationToken);
        var productTypesByNameExists = await storage.GetProductTypesByName(productTypesByNameQuery.ProductTypeName, cancellationToken);
        
        if (productTypesByNameExists is null)
            throw new NotFoundByNameException(productTypesByNameQuery.ProductTypeName, "Тип готового мебельного изделия");
        
        return productTypesByNameExists;
    }
}