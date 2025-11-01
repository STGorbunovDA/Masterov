using AutoMapper;
using Masterov.Domain.Masterov.Supplier.GetSupplierByEmail;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Supplier;

internal class GetSupplierByEmailStorage(MasterovDbContext dbContext, IMapper mapper)
    : IGetSupplierByEmailStorage
{
    public async Task<SupplierDomain?> GetSupplierByEmail(string supplierEmail, CancellationToken cancellationToken)
    {
        var normalizedEmail = supplierEmail.Trim().ToLower();

        var supplier = await dbContext.Suppliers
            .AsNoTracking()
                .Include(c => c.Supplies)
                    .ThenInclude(p => p.ComponentType)
                .Include(c => c.Supplies)
                    .ThenInclude(p => p.Warehouse)
            .FirstOrDefaultAsync(f => f.Email != null && f.Email.ToLower() == normalizedEmail, cancellationToken);

        return mapper.Map<SupplierDomain?>(supplier);
    }
}