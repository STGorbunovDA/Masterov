using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.Payment.GetCustomerByPaymentId;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Payment;

internal class GetCustomerByPaymentIdStorage(MasterovDbContext dbContext, IMapper mapper) : IGetCustomerByPaymentIdStorage
{
    public async Task<CustomerDomain?> GetCustomerByPaymentId(Guid paymentId, CancellationToken cancellationToken)
    {
        return await dbContext.Payments
            .AsNoTracking() 
            .Where(o => o.PaymentId == paymentId)
            .Select(o => o.Customer)
            .ProjectTo<CustomerDomain>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }
}