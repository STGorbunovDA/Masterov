using Masterov.Domain.Masterov.Supply.GetSupplierBySupplyId.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supply.GetSupplierBySupplyId;

public interface IGetSupplierBySupplyIdUseCase
{
    Task<SupplierDomain?> Execute(GetSupplierBySupplyIdQuery getSupplierBySupplyIdQuery, CancellationToken cancellationToken);
}