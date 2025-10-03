using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.Payment.GetCustomerByPaymentId;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Masterov.Storage.Storages.Masterov.Payment;

internal class GetCustomerByPaymentIdStorage(MasterovDbContext dbContext, IMemoryCache memoryCache, IMapper mapper) : IGetCustomerByPaymentIdStorage
{
    public async Task<CustomerDomain?> GetCustomerByPaymentId(Guid paymentId, CancellationToken cancellationToken) =>
        await memoryCache.GetOrCreateAsync(
            nameof(GetCustomerByPaymentId),
            async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(1);

                return await dbContext.Payments
                    .AsNoTracking() 
                    .Where(o => o.PaymentId == paymentId)
                    .Select(o => o.Customer)
                    .ProjectTo<CustomerDomain>(mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(cancellationToken);
            });
}