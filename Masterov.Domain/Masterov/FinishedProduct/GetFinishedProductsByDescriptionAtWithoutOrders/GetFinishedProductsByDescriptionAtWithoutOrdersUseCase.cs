using FluentValidation;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByDescriptionAtWithoutOrders.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByDescriptionAtWithoutOrders;

public class GetFinishedProductsByDescriptionAtWithoutOrdersUseCase(IValidator<GetFinishedProductsByDescriptionAtWithoutOrdersQuery> validator,
    IGetFinishedProductsByDescriptionAtWithoutOrdersStorage storage) : IGetFinishedProductsByDescriptionAtWithoutOrdersUseCase
{
    public async Task<IEnumerable<FinishedProductNoOrdersDomain>?> Execute(GetFinishedProductsByDescriptionAtWithoutOrdersQuery query,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(query, cancellationToken);
        return await storage.GetFinishedProductsByDescriptionAtWithoutOrders(query.Description, cancellationToken);
    }
}