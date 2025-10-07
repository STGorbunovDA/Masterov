using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.Payment.GetOrderByPaymentId;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Payment;

internal class GetOrderByPaymentIdStorage(MasterovDbContext dbContext, IMapper mapper) : IGetOrderByPaymentIdStorage
{
    public async Task<OrderDomain?> GetOrderByPaymentId(Guid paymentId, CancellationToken cancellationToken)
    {
        return await dbContext.Payments
            .AsNoTracking() 
            .Where(o => o.PaymentId == paymentId)
            .Select(o => o.Order)
            .ProjectTo<OrderDomain>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }
}