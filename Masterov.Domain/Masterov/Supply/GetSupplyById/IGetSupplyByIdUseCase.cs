using Masterov.Domain.Masterov.Supply.GetSupplyById.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supply.GetSupplyById;

public interface IGetSupplyByIdUseCase
{
    Task<SupplyDomain?> Execute(GetSupplyByIdQuery getSupplyByIdQuery, CancellationToken cancellationToken);
}