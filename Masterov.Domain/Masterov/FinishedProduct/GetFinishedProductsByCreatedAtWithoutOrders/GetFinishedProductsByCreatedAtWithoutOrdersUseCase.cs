using FluentValidation;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByCreatedAtWithoutOrders.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByCreatedAtWithoutOrders;

public class GetFinishedProductsByCreatedAtWithoutOrdersUseCase(IValidator<GetFinishedProductsByCreatedAtWithoutOrdersQuery> validator,
    IGetFinishedProductsByCreatedAtWithoutOrdersStorage storage) 
    : IGetFinishedProductsByCreatedAtWithoutOrdersUseCase
{
    public async Task<IEnumerable<FinishedProductWithoutOrdersDomain>?> Execute(GetFinishedProductsByCreatedAtWithoutOrdersQuery finishedProductsByCreatedAtQuery,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(finishedProductsByCreatedAtQuery, cancellationToken);
        
        return await storage.GetFinishedProductsByCreatedAtWithoutOrders(finishedProductsByCreatedAtQuery.CreatedAt, cancellationToken);
    }
}