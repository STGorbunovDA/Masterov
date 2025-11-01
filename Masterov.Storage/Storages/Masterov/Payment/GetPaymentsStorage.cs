using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.Payment.GetPayments;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Payment;

internal class GetPaymentsStorage (MasterovDbContext dbContext, IMapper mapper) : IGetPaymentsStorage
{
    public async Task<IEnumerable<PaymentDomain?>> GetPayments(CancellationToken cancellationToken) =>
        await dbContext.Payments
            .AsNoTracking() 
            .ProjectTo<PaymentDomain>(mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);
}