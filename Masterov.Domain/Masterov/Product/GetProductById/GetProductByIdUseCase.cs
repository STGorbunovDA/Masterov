using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Product.GetProductById.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Product.GetProductById;

public class GetProductByIdUseCase(IValidator<GetProductByIdQuery> validator, IGetProductByIdStorage storage) : IGetProductByIdUseCase
{
    public async Task<ProductDomain?> Execute(GetProductByIdQuery getProductByIdQuery, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(getProductByIdQuery, cancellationToken);
        var productExists = await storage.GetProductById(getProductByIdQuery.ProductId, cancellationToken);
        
        if (productExists is null)
            throw new NotFoundByIdException(getProductByIdQuery.ProductId, "Изделие");
        
        return productExists;
    }
}