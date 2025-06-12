using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductById;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductOrders.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductOrders;

public class GetFinishedProductOrdersUseCase(IValidator<GetFinishedProductOrdersQuery> validator,
    IGetFinishedProductOrdersStorage storage, IGetFinishedProductByIdStorage getFinishedProductByIdStorage) 
    : IGetFinishedProductOrdersUseCase
{
    public async Task<IEnumerable<ProductionOrderDomain>?> Execute(GetFinishedProductOrdersQuery getFinishedProductOrdersQuery,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(getFinishedProductOrdersQuery, cancellationToken);

        var finishedProductExists = await getFinishedProductByIdStorage.GetFinishedProductById(getFinishedProductOrdersQuery.FinishedProductId, cancellationToken);
        
        if (finishedProductExists is null)
            throw new NotFoundByIdException(getFinishedProductOrdersQuery.FinishedProductId, "Готовое мебельное изделие");

        return await storage.GetFinishedProductOrders(getFinishedProductOrdersQuery.FinishedProductId,
            getFinishedProductOrdersQuery.CreatedAt, getFinishedProductOrdersQuery.CompletedAt,
            getFinishedProductOrdersQuery.Status, getFinishedProductOrdersQuery.Description, cancellationToken);
    }
}