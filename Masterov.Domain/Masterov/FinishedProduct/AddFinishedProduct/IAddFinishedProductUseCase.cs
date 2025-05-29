using Masterov.Domain.Masterov.FinishedProduct.AddFinishedProduct.Command;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.FinishedProduct.AddFinishedProduct;

public interface IAddFinishedProductUseCase
{
    Task<FinishedProductDomain> Execute(AddFinishedProductCommand addFinishedProductCommand, CancellationToken cancellationToken);
}