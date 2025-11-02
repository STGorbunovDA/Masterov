using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.Supply.GetSupplierBySupplyId;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Supply;

internal class GetSupplierBySupplyIdStorage(MasterovDbContext dbContext, IMapper mapper)
    : IGetSupplierBySupplyIdStorage
{
    public async Task<SupplierDomain?> GetSupplierBySupplyId(Guid supplyId, CancellationToken cancellationToken) =>
        await dbContext.Supplies
            .AsNoTracking()
            .Where(o => o.SupplyId == supplyId)
            .Select(o => o.Supplier)
            .ProjectTo<SupplierDomain>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
}