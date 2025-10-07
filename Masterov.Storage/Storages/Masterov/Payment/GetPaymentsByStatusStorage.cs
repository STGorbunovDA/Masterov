using AutoMapper;
using Masterov.Domain.Extension;
using Masterov.Domain.Masterov.Payment.GetPaymentsByStatus;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Payment;

public class GetPaymentsByStatusStorage (MasterovDbContext dbContext, IMapper mapper) : IGetPaymentsByStatusStorage
{
    public async Task<IEnumerable<PaymentDomain>?> GetPaymentsByStatus(PaymentMethod paymentMethod, CancellationToken cancellationToken)
    {
        var orders = await dbContext.Payments
            .Where(payMethod => payMethod.MethodPayment == paymentMethod)
            .Include(o => o.Customer)
                .ThenInclude(c => c.Orders)
            .ToArrayAsync(cancellationToken);

        return mapper.Map<IEnumerable<PaymentDomain>>(orders);
    }
}