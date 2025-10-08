using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductByNameWithoutOrders.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductByNameWithoutOrders;

public class GetFinishedProductByNameWithoutOrdersUseCase(IValidator<GetFinishedProductByNameWithoutOrdersQuery> validator, IGetFinishedProductByNameWithoutOrdersStorage storage) : IGetFinishedProductByNameWithoutOrdersUseCase
{
    public async Task<FinishedProductWithoutOrdersDomain?> Execute(GetFinishedProductByNameWithoutOrdersQuery getFinishedProductByNameQuery, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(getFinishedProductByNameQuery, cancellationToken);
        var finishedProductExists = await storage.GetFinishedProductByNameWithoutOrders(getFinishedProductByNameQuery.FinishedProductName, cancellationToken);
        
        if (finishedProductExists is null)
            throw new NotFoundByNameException(getFinishedProductByNameQuery.FinishedProductName, "Готовое мебельное изделие");
        
        return finishedProductExists;
    }
}