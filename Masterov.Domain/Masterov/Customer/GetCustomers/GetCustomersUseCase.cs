using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Customer.GetCustomers;

public class GetCustomersUseCase(IGetCustomersStorage storage) : IGetCustomersUseCase
{
    public async Task<IEnumerable<CustomerDomain>> Execute(CancellationToken cancellationToken)
    {
        var res =  await storage.GetCustomers(cancellationToken);

        return res;
    }
      
}