using AutoMapper;
using Masterov.Domain.Masterov.Supplier.GetSuppliersBySurname;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Supplier;

internal class GetSuppliersBySurnameStorage(MasterovDbContext dbContext, IMapper mapper) : IGetSuppliersBySurnameStorage
{
    public async Task<IEnumerable<SupplierDomain?>> GetSuppliersBySurname(string supplierName, CancellationToken cancellationToken)
    {
        var normalizedName = supplierName.Trim().ToLower();

        var suppliers = await dbContext.Suppliers
            .AsNoTracking()
                .Include(c => c.Supplies)
                    .ThenInclude(p => p.ComponentType)
                .Include(c => c.Supplies)
                    .ThenInclude(p => p.Warehouse)
            .Where(f => f.Surname.ToLower().Contains(normalizedName))
            .ToArrayAsync(cancellationToken);

        return mapper.Map<IEnumerable<SupplierDomain>>(suppliers);
    }
}