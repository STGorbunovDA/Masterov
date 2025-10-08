using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductByIdWithoutOrders.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductByIdWithoutOrders;

public class GetFinishedProductByIdWithoutOrdersUseCase(IValidator<GetFinishedProductByIdWithoutOrdersQuery> validator, IGetFinishedProductByIdWithoutOrdersStorage storage) : IGetFinishedProductByIdWithoutOrdersUseCase
{
    public async Task<FinishedProductWithoutOrdersDomain?> Execute(GetFinishedProductByIdWithoutOrdersQuery getFinishedProductByIdQuery, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(getFinishedProductByIdQuery, cancellationToken);
        var finishedProductExists = await storage.GetFinishedProductByIdWithoutOrders(getFinishedProductByIdQuery.FinishedProductId, cancellationToken);
        
        if (finishedProductExists is null)
            throw new NotFoundByIdException(getFinishedProductByIdQuery.FinishedProductId, "Готовое мебельное изделие");
        
        return finishedProductExists;
    }
}