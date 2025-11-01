using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByNameWithoutOrders.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByNameWithoutOrders;

public class GetFinishedProductsByNameWithoutOrdersUseCase(IValidator<GetFinishedProductsByNameWithoutOrdersQuery> validator, IGetFinishedProductsByNameWithoutOrdersStorage storage) : IGetFinishedProductsByNameWithoutOrdersUseCase
{
    public async Task<IEnumerable<FinishedProductNoOrdersDomain?>> Execute(GetFinishedProductsByNameWithoutOrdersQuery getFinishedProductsByNameQuery, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(getFinishedProductsByNameQuery, cancellationToken);
        var finishedProductExists = await storage.GetFinishedProductByNameWithoutOrders(getFinishedProductsByNameQuery.FinishedProductName, cancellationToken);
        
        if (finishedProductExists is null)
            throw new NotFoundByNameException(getFinishedProductsByNameQuery.FinishedProductName, "Готовое мебельное изделие");
        
        return finishedProductExists;
    }
}