using AutoMapper;
using Masterov.Domain.Masterov.Payment.GetPaymentsByUpdatedAt;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Payment;

internal class GetPaymentsByUpdatedAtStorage (MasterovDbContext dbContext, IMapper mapper) : IGetPaymentsByUpdatedAtStorage
{
    public async Task<IEnumerable<PaymentDomain>?> GetPaymentsByUpdatedAt(DateTime? updatedAt, CancellationToken cancellationToken)
    {
        if (!updatedAt.HasValue)
            return null;
        
        var startOfDay = updatedAt.Value.Date;
        var endOfDay = startOfDay.AddDays(1);
        
        var payments = await dbContext.Payments
            .AsNoTracking() 
            .Where(payDate => payDate.UpdatedAt >= startOfDay && payDate.UpdatedAt < endOfDay)
            .Include(o => o.Customer)
                .ThenInclude(c => c.Orders)
            .ToArrayAsync(cancellationToken);

        return mapper.Map<IEnumerable<PaymentDomain>>(payments);
    }
}