using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.Payment.GetPayments;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Masterov.Storage.Storages.Masterov.Payment;

internal class GetPaymentsStorage (MasterovDbContext dbContext, IMemoryCache memoryCache, IMapper mapper) : IGetPaymentsStorage
{
    public async Task<IEnumerable<PaymentDomain>> GetPayments(CancellationToken cancellationToken) =>
        (await memoryCache.GetOrCreateAsync<PaymentDomain[]>(
            nameof(GetPayments),
            entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(1);
                return dbContext.OrderPayments
                    .ProjectTo<PaymentDomain>(mapper.ConfigurationProvider)
                    .ToArrayAsync(cancellationToken);
            }))!;
}