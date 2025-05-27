using Masterov.Domain.Masterov.Product.GetProductById.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Product.GetProductById;

public interface IGetProductByIdUseCase
{
    Task<ProductDomain?> Execute(GetProductByIdQuery getProductByIdQuery, CancellationToken cancellationToken);
}