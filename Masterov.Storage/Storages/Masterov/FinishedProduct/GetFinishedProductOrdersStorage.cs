using AutoMapper;
using Masterov.Domain.Extension;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductOrders;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.FinishedProduct;

public class GetFinishedProductOrdersStorage (MasterovDbContext dbContext, IMapper mapper) : IGetFinishedProductOrdersStorage
{
    public async Task<IEnumerable<ProductionOrderDomain>?> GetFinishedProductOrders(
        Guid finishedProductId,
        DateTime? createdAt,
        DateTime? completedAt,
        ProductionOrderStatus status,
        string? description,
        CancellationToken cancellationToken)
    {
        var query = dbContext.ProductionOrders
            .Where(o => o.FinishedProductId == finishedProductId)
            .AsQueryable();

        // Фильтрация по дате создания (если указана)
        if (createdAt.HasValue)
        {
            query = query.Where(o => o.CreatedAt.Date == createdAt.Value.Date);
        }

        // Фильтрация по дате завершения (если указана)
        if (completedAt.HasValue)
        {
            query = query.Where(o => o.CompletedAt.HasValue && o.CompletedAt.Value.Date == completedAt.Value.Date);
        }

        // Фильтрация по статусу (если явно задан, отличному от Draft)
        if (status != ProductionOrderStatus.Draft)
        {
            query = query.Where(o => o.Status == status);
        }

        // Фильтрация по описанию (если указано)
        if (!string.IsNullOrWhiteSpace(description))
        {
            var loweredDescription = description.ToLower();
            query = query.Where(o => o.Description != null && o.Description.ToLower().Contains(loweredDescription));
        }

        var orders = await query
            .AsNoTracking() 
            .Include(o => o.Components)
            .ThenInclude(c => c.ProductType)
            .Include(o => o.Components)
            .ThenInclude(c => c.Warehouse)
            .Include(o => o.Customer)
            .Include(o => o.Payments)
            .ToArrayAsync(cancellationToken);

        return orders.Select(mapper.Map<ProductionOrderDomain>);
    }
}