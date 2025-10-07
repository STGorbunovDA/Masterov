using AutoMapper;
using Masterov.Domain.Masterov.Payment.GetPaymentsByOrderId;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Masterov.Storage.Storages.Masterov.Payment;

public class GetPaymentsByOrderIdStorage (MasterovDbContext dbContext, IMemoryCache memoryCache, IMapper mapper) : IGetPaymentsByOrderIdStorage
{
    public async Task<IEnumerable<PaymentDomain>?> GetPaymentsByOrderId(Guid orderId, CancellationToken cancellationToken)
    {
        var payments = await dbContext.Payments
            .AsNoTracking()
            .Include(p => p.Customer)
            .Where(p => p.OrderId == orderId)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync(cancellationToken);

        if (payments.Count == 0)
        {
            return null;
        }

        return mapper.Map<IEnumerable<PaymentDomain>>(payments);
    }
}