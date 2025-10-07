using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.Payment.GetPaymentById;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Payment;

internal class GetPaymentByIdStorage(MasterovDbContext dbContext, IMapper mapper) : IGetPaymentByIdStorage
{
    public async Task<PaymentDomain?> GetPaymentById(Guid paymentId, CancellationToken cancellationToken)
    {
        return await dbContext.Payments
            .AsNoTracking() 
            .Where(f => f.PaymentId == paymentId)
            .ProjectTo<PaymentDomain>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }
}