using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Product.GetProductById;
using Masterov.Domain.Masterov.Product.GetProductById.Query;
using Masterov.Domain.Masterov.ProductType.GetProductTypeById.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductType.GetProductTypeById;

public class GetProductTypeByIdUseCase(IValidator<GetProductTypeByIdQuery> validator, IGetProductTypeByIdStorage storage) : IGetProductTypeByIdUseCase
{
    public async Task<ProductTypeDomain?> Execute(GetProductTypeByIdQuery getProductTypeByIdQuery, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(getProductTypeByIdQuery, cancellationToken);
        var productTypeExists = await storage.GetProductTypeById(getProductTypeByIdQuery.ProductTypeId, cancellationToken);
        
        if (productTypeExists is null)
            throw new NotFoundByIdException(getProductTypeByIdQuery.ProductTypeId, "Тип изделия");
        
        return productTypeExists;
    }
}