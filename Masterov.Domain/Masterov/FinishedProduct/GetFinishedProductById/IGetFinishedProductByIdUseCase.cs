using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductById.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductById;

public interface IGetFinishedProductByIdUseCase
{
    Task<FinishedProductDomain?> Execute(GetFinishedProductByIdQuery getFinishedProductByIdQuery, CancellationToken cancellationToken);
}