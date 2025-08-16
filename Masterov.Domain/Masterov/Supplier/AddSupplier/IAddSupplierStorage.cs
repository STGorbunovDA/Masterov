using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supplier.AddSupplier;

public interface IAddSupplierStorage
{
    Task<SupplierDomain> AddSupplier(string name, string? address, string? phone, CancellationToken cancellationToken);
}