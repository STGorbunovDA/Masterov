using AutoMapper;
using Masterov.Domain.Masterov.Payment.GetPaymentsByAmount;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Payment;

internal class GetPaymentsByAmountStorage(MasterovDbContext dbContext, IMapper mapper) : IGetPaymentsByAmountStorage
{
    public async Task<IEnumerable<PaymentDomain?>> GetPaymentsByAmount(decimal amount, CancellationToken cancellationToken)
    {
        var payments = await dbContext.Payments
            .AsNoTracking() 
            .Where(p => p.Amount == amount)
            .Include(o => o.Customer)
                .ThenInclude(o => o.Orders)
            .ToArrayAsync(cancellationToken);

        return mapper.Map<IEnumerable<PaymentDomain>>(payments);
    }
}