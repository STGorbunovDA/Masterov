using Masterov.Domain.Masterov.ProductType.AddProductType.Command;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductType.AddProductType;

public interface IAddProductTypeUseCase
{
    Task<ProductTypeDomain> Execute(AddProductTypeCommand addProductTypeCommand, CancellationToken cancellationToken);
}