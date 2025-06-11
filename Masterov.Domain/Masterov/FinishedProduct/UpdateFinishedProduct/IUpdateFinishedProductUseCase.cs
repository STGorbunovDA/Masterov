using Masterov.Domain.Masterov.FinishedProduct.UpdateFinishedProduct.Command;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.FinishedProduct.UpdateFinishedProduct;

public interface IUpdateFinishedProductUseCase
{
    Task<FinishedProductDomain> Execute(UpdateFinishedProductCommand command, CancellationToken cancellationToken);
}