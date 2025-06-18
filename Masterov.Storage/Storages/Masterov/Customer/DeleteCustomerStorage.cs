using Masterov.Domain.Masterov.Customer.DeleteCustomer;

namespace Masterov.Storage.Storages.Masterov.Customer;

internal class DeleteCustomerStorage(MasterovDbContext dbContext) : IDeleteCustomerStorage
{
    public async Task<bool> DeleteCustomer(Guid сustomerId, CancellationToken cancellationToken)
    {
        var customer = await dbContext.Set<Storage.Customer>().FindAsync(new object[] { сustomerId }, cancellationToken);
        
        if (customer == null)
            return false;
        
        dbContext.Set<Storage.Customer>().Remove(customer);
        await dbContext.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}