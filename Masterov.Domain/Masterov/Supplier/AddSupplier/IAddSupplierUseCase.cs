using Masterov.Domain.Masterov.Supplier.AddSupplier.Command;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supplier.AddSupplier;

public interface IAddSupplierUseCase
{
    Task<SupplierDomain> Execute(AddSupplierCommand addSupplierCommand, CancellationToken cancellationToken);
}