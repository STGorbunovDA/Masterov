using Masterov.Domain.Masterov.ProductType.DeleteProductType.Command;

namespace Masterov.Domain.Masterov.ProductType.DeleteProductType;

public interface IDeleteProductTypeUseCase
{
    Task<bool> Execute(DeleteProductTypeCommand deleteProductTypeCommand, CancellationToken cancellationToken);
}