using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supplier.UpdateSupplier;

public interface IUpdateSupplierStorage
{
    Task<SupplierDomain> UpdateSupplier(Guid supplierId, string name, string? address, string? phone, CancellationToken cancellationToken);
}