using Masterov.Domain.Masterov.Supplier.UpdateSupplier.Command;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supplier.UpdateSupplier;

public interface IUpdateSupplierUseCase
{
    Task<SupplierDomain> Execute(UpdateSupplierCommand command, CancellationToken cancellationToken);
}