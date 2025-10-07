using AutoMapper;
using Masterov.Domain.Masterov.Payment.GetPaymentsByCreatedAt;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Payment;

internal class GetPaymentsByCreatedAtStorage (MasterovDbContext dbContext, IMapper mapper) : IGetPaymentsByCreatedAtStorage
{
    public async Task<IEnumerable<PaymentDomain>?> GetPaymentsByCreatedAt(DateTime? createdAt, CancellationToken cancellationToken)
    {
        if (!createdAt.HasValue)
            return null;
        
        var startOfDay = createdAt.Value.Date;
        var endOfDay = startOfDay.AddDays(1);
        
        var payments = await dbContext.Payments
            .AsNoTracking() 
            .Where(payDate => payDate.CreatedAt >= startOfDay && payDate.CreatedAt < endOfDay)
            .Include(o => o.Customer)
                .ThenInclude(c => c.Orders)
            .ToArrayAsync(cancellationToken);

        return mapper.Map<IEnumerable<PaymentDomain>>(payments);
    }
}