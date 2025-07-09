using AutoMapper;
using Masterov.Domain.Extension;
using Masterov.Domain.Masterov.Payment.UpdatePayment;
using Masterov.Domain.Models;

namespace Masterov.Storage.Storages.Masterov.Payment;

internal class UpdatePaymentStorage(MasterovDbContext dbContext, IMapper mapper) : IUpdatePaymentStorage
{
    public async Task<PaymentDomain> UpdatePayment(Guid paymentId, Guid orderId, Guid customerId, 
        PaymentMethod methodPayment, decimal amount, DateTime paymentDate, CancellationToken cancellationToken)
    {
        var paymentExists = await dbContext.Set<Storage.OrderPayment>().FindAsync([paymentId], cancellationToken);
        
        if (paymentExists == null)
            throw new Exception("payment not found");
        
        paymentExists.OrderId = orderId;
        paymentExists.CustomerId = customerId;
        paymentExists.MethodPayment = methodPayment;
        paymentExists.Amount = amount;
        paymentExists.PaymentDate = paymentDate;
        
        await dbContext.SaveChangesAsync(cancellationToken);

        return mapper.Map<PaymentDomain>(paymentExists);
    }
}