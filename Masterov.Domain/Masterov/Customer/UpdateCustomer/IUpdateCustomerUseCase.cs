using Masterov.Domain.Masterov.Customer.UpdateCustomer.Command;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Customer.UpdateCustomer;

public interface IUpdateCustomerUseCase
{
    Task<CustomerDomain> Execute(UpdateCustomerCommand command, CancellationToken cancellationToken);
}