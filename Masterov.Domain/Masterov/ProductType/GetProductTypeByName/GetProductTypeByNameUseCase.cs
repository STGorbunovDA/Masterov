using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.ProductType.GetProductTypeByName.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductType.GetProductTypeByName;

public class GetProductTypeByNameUseCase(IValidator<GetProductTypeByNameQuery> validator, IGetProductTypeByNameStorage storage) : IGetProductTypeByNameUseCase
{
    public async Task<ProductTypeDomain?> Execute(GetProductTypeByNameQuery getProductTypeByNameQuery, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(getProductTypeByNameQuery, cancellationToken);
        var productTypeExists = await storage.GetProductTypeByName(getProductTypeByNameQuery.ProductTypeName, cancellationToken);
        
        if (productTypeExists is null)
            throw new NotFoundByNameException(getProductTypeByNameQuery.ProductTypeName, "Тип изделия");
        
        return productTypeExists;
    }
}