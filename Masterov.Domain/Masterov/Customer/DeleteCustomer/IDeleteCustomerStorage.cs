namespace Masterov.Domain.Masterov.Customer.DeleteCustomer;

public interface IDeleteCustomerStorage
{
    Task<bool> DeleteCustomer(Guid CustomerId, CancellationToken cancellationToken);
}