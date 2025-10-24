using AutoMapper;
using Masterov.Domain.Masterov.Supply.GetSuppliesBySupplyDate;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Supply;

public class GetSuppliesBySupplyDateStorage (MasterovDbContext dbContext, IMapper mapper) : IGetSuppliesBySupplyDateStorage
{
    public async Task<IEnumerable<SupplyDomain>?> GetSuppliesBySupplyDate(DateTime supplyDate, CancellationToken cancellationToken)
    {
        // Фильтрация по дате (без учёта времени)
        var startOfDay = supplyDate.Date;
        var endOfDay = startOfDay.AddDays(1);

        var payments = await dbContext.Supplies
            .AsNoTracking() 
            .Where(payDate => payDate.SupplyDate >= startOfDay && payDate.SupplyDate < endOfDay)
                .Include(c => c.ComponentType)
                .Include(o => o.Warehouse) 
                    .ThenInclude(w => w.ComponentType)
                .Include(c => c.Supplier)
            .ToArrayAsync(cancellationToken);

        return mapper.Map<IEnumerable<SupplyDomain>>(payments);
    }
}