using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.Payment.GetPaymentById;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Masterov.Storage.Storages.Masterov.Payment;

internal class GetPaymentByIdStorage(MasterovDbContext dbContext, IMemoryCache memoryCache, IMapper mapper) : IGetPaymentByIdStorage
{
    public async Task<PaymentDomain?> GetPaymentById(Guid customerId, CancellationToken cancellationToken) =>
        (await memoryCache.GetOrCreateAsync<PaymentDomain?>( 
            nameof(GetPaymentById),
            entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(1);
                return dbContext.OrderPayments
                    .Where(f => f.PaymentId == customerId)
                    .ProjectTo<PaymentDomain>(mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(cancellationToken);
            }))!;
}