using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.Payment.GetProductionOrderByPaymentId;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Masterov.Storage.Storages.Masterov.Payment;

internal class GetProductionOrderByPaymentIdStorage(MasterovDbContext dbContext, IMemoryCache memoryCache, IMapper mapper) : IGetProductionOrderByPaymentIdStorage
{
    public async Task<ProductionOrderDomain?> GetProductionOrderByPaymentId(Guid paymentId, CancellationToken cancellationToken)=>
        await memoryCache.GetOrCreateAsync(
            nameof(GetProductionOrderByPaymentId),
            async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(1);

                return await dbContext.Payments
                    .AsNoTracking() 
                    .Where(o => o.PaymentId == paymentId)
                    .Select(o => o.Order)
                    .ProjectTo<ProductionOrderDomain>(mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(cancellationToken);
            });
}