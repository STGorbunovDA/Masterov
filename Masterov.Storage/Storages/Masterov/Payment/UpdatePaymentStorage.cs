using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Extension;
using Masterov.Domain.Masterov.Payment.UpdatePayment;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Payment;

internal class UpdatePaymentStorage(MasterovDbContext dbContext, IMapper mapper) : IUpdatePaymentStorage
{
    public async Task<PaymentDomain> UpdatePayment(Guid paymentId, Guid orderId, Guid customerId, 
        PaymentMethod methodPayment, decimal amount, DateTime? createdAt, CancellationToken cancellationToken)
    {
        var paymentExists = await dbContext.Set<Storage.Payment>().FindAsync([paymentId], cancellationToken);
        
        if (paymentExists == null)
            throw new Exception("payment not found");
        
        paymentExists.OrderId = orderId;
        paymentExists.CustomerId = customerId;
        paymentExists.MethodPayment = methodPayment;
        paymentExists.Amount = amount;
        if (createdAt.HasValue)
            paymentExists.CreatedAt = createdAt.Value;
        paymentExists.UpdatedAt = DateTime.Now;
        
        await dbContext.SaveChangesAsync(cancellationToken);
        
       var payment = await dbContext.Payments
            .AsNoTracking() 
            .Where(f => f.PaymentId == paymentId)
            .ProjectTo<PaymentDomain>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        return mapper.Map<PaymentDomain>(payment);
    }
}