using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supplier.AddSupplier;

public interface IAddSupplierStorage
{
    Task<SupplierDomain> AddSupplier(string name, string surname, string? email, string? phone, string? address, CancellationToken cancellationToken);
}