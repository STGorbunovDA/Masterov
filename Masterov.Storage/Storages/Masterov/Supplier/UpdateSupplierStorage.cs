using AutoMapper;
using Masterov.Domain.Masterov.Supplier.UpdateSupplier;
using Masterov.Domain.Models;

namespace Masterov.Storage.Storages.Masterov.Supplier;

internal class UpdateSupplierStorage(MasterovDbContext dbContext, IMapper mapper) : IUpdateSupplierStorage
{
    public async Task<SupplierDomain> UpdateSupplier(Guid supplierId, string name, string surname, string? email, string? phone, string? address, DateTime? createdAt,
        CancellationToken cancellationToken)
    {
        var supplierExists = await dbContext.Set<Storage.Supplier>().FindAsync([supplierId], cancellationToken);
        
        if (supplierExists == null)
            throw new Exception("supplier not found");
        
        supplierExists.Name = name;
        supplierExists.Surname = surname;
        supplierExists.Email = email;
        supplierExists.Phone = phone;
        supplierExists.Address = address;
        if (createdAt.HasValue)
            supplierExists.CreatedAt = createdAt.Value;
        supplierExists.UpdatedAt = DateTime.Now;
        
        await dbContext.SaveChangesAsync(cancellationToken);

        return mapper.Map<SupplierDomain>(supplierExists);
    }
}