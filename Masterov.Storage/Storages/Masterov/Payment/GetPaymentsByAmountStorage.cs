using AutoMapper;
using Masterov.Domain.Masterov.Payment.GetPaymentsByAmount;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Masterov.Storage.Storages.Masterov.Payment;

internal class GetPaymentsByAmountStorage(MasterovDbContext dbContext, IMemoryCache memoryCache, IMapper mapper) : IGetPaymentsByAmountStorage
{
    public async Task<IEnumerable<PaymentDomain?>> GetPaymentsByAmount(decimal amount, CancellationToken cancellationToken)
    {
        var payments = await dbContext.Payments
            .AsNoTracking() 
            .Where(p => p.Amount == amount)
            .Include(o => o.Customer)
            .ToArrayAsync(cancellationToken);

        return mapper.Map<IEnumerable<PaymentDomain>>(payments);
    }
}