using Masterov.Domain.Masterov.Customer.AddCustomer.Command;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Customer.AddCustomer;

public interface IAddCustomerUseCase
{
    Task<CustomerDomain> Execute(AddCustomerCommand addCustomerCommand, CancellationToken cancellationToken);
}