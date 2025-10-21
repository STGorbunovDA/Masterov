using FluentValidation;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByUpdatedAtWithoutOrders.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByUpdatedAtWithoutOrders;

public class GetFinishedProductsByUpdatedAtWithoutOrdersUseCase(IValidator<GetFinishedProductsByUpdatedAtWithoutOrdersQuery> validator,
    IGetFinishedProductsByUpdatedAtWithoutOrdersStorage storage) 
    : IGetFinishedProductsByUpdatedAtWithoutOrdersUseCase
{
    public async Task<IEnumerable<FinishedProductNoOrdersDomain>?> Execute(GetFinishedProductsByUpdatedAtWithoutOrdersQuery finishedProductsByUpdatedAtQuery,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(finishedProductsByUpdatedAtQuery, cancellationToken);
        
        return await storage.GetFinishedProductsByUpdatedAtWithoutOrders(finishedProductsByUpdatedAtQuery.UpdatedAt, cancellationToken);
    }
}