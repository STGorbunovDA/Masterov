using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.ProductType.GetProductTypeById.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductType.GetProductTypeById;

public class GetProductTypeByIdUseCase(IValidator<GetProductTypeByIdQuery> validator, IGetProductTypeByIdStorage storage) : IGetProductTypeByIdUseCase
{
    public async Task<ProductTypeDomain?> Execute(GetProductTypeByIdQuery productTypeByIdQuery, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(productTypeByIdQuery, cancellationToken);
        var productTypeExists = await storage.GetProductTypeById(productTypeByIdQuery.ProductTypeById, cancellationToken);
        
        if (productTypeExists is null)
            throw new NotFoundByIdException(productTypeByIdQuery.ProductTypeById, "Тип мебельного изделия");
        
        return productTypeExists;
    }
}