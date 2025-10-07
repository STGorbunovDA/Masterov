using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Extension;
using Masterov.Domain.Masterov.Payment.AddPayment;
using Masterov.Domain.Models;
using Masterov.Storage.Extension;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Payment;

internal class AddPaymentStorage(MasterovDbContext dbContext, IGuidFactory guidFactory, IMapper mapper) : IAddPaymentStorage
{
    public async Task<PaymentDomain> AddPayment(Guid orderId, PaymentMethod paymentMethod, decimal amount, Guid customerId, CancellationToken cancellationToken)
    {
        var paymentId = guidFactory.Create();

        var payment = new Storage.Payment()
        {
            PaymentId = paymentId,
            CustomerId = customerId,
            MethodPayment = paymentMethod,
            Amount = amount,
            OrderId = orderId,
            CreatedAt = DateTime.Now
        };
        
        await dbContext.Payments.AddAsync(payment, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return await dbContext.Payments
            .Where(t => t.PaymentId == paymentId)
            .ProjectTo<PaymentDomain>(mapper.ConfigurationProvider)
            .FirstAsync(cancellationToken);
    }
}