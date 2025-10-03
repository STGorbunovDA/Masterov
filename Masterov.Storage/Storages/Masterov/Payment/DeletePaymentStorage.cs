using Masterov.Domain.Masterov.Payment.DeletePayment;

namespace Masterov.Storage.Storages.Masterov.Payment;

internal class DeletePaymentStorage(MasterovDbContext dbContext) : IDeletePaymentStorage
{
    public async Task<bool> DeletePayment(Guid paymentId, CancellationToken cancellationToken)
    {
        var payment = await dbContext.Set<Storage.Payment>().FindAsync(new object[] { paymentId }, cancellationToken);
        
        if (payment == null)
            return false;
        
        dbContext.Set<Storage.Payment>().Remove(payment);
        await dbContext.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}