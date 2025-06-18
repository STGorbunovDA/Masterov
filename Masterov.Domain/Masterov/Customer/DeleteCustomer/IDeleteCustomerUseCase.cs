using Masterov.Domain.Masterov.Customer.DeleteCustomer.Command;

namespace Masterov.Domain.Masterov.Customer.DeleteCustomer;

public interface IDeleteCustomerUseCase
{
    Task<bool> Execute(DeleteCustomerCommand deleteCustomerCommand, CancellationToken cancellationToken);
}