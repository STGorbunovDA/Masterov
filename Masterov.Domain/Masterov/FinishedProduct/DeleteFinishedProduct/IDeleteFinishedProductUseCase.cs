using Masterov.Domain.Masterov.FinishedProduct.DeleteFinishedProduct.Command;

namespace Masterov.Domain.Masterov.FinishedProduct.DeleteFinishedProduct;

public interface IDeleteFinishedProductUseCase
{
    Task<bool> Execute(DeleteFinishedProductCommand deleteFinishedProductCommand, CancellationToken cancellationToken);
}