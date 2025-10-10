using AutoMapper;
using Masterov.Domain.Masterov.Payment.GetPaymentsByOrderId;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Payment;

public class GetPaymentsByOrderIdStorage (MasterovDbContext dbContext, IMapper mapper) : IGetPaymentsByOrderIdStorage
{
    public async Task<IEnumerable<PaymentDomain>?> GetPaymentsByOrderId(Guid orderId, CancellationToken cancellationToken)
    {
        var payments = await dbContext.Payments
            .AsNoTracking()
            .Include(p => p.Customer)
            .Where(p => p.OrderId == orderId)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync(cancellationToken);

        return mapper.Map<IEnumerable<PaymentDomain>>(payments);
    }
}