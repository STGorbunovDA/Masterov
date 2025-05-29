using Masterov.Domain.Masterov.ProductType.UpdateProductType.Command;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductType.UpdateProductType;

public interface IUpdateProductTypeUseCase
{
    Task<ProductTypeDomain> Execute(UpdateProductTypeCommand command, CancellationToken cancellationToken);
}