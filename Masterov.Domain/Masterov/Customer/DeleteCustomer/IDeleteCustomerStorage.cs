namespace Masterov.Domain.Masterov.Customer.DeleteCustomer;

public interface IDeleteCustomerStorage
{
    Task<bool> DeleteCustomer(Guid сustomerId, CancellationToken cancellationToken);
}