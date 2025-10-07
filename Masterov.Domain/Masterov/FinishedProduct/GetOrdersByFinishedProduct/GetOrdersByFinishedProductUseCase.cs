using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductById;
using Masterov.Domain.Masterov.FinishedProduct.GetOrdersByFinishedProduct.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.FinishedProduct.GetOrdersByFinishedProduct;

public class GetOrdersByFinishedProductUseCase(IValidator<GetOrdersByFinishedProductQuery> validator,
    IGetOrdersByFinishedProductStorage storage, IGetFinishedProductByIdStorage getFinishedProductByIdStorage) 
    : IGetOrdersByFinishedProductUseCase
{
    public async Task<IEnumerable<OrderDomain>?> Execute(GetOrdersByFinishedProductQuery getOrdersByFinishedProductQuery,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(getOrdersByFinishedProductQuery, cancellationToken);

        var finishedProductExists = await getFinishedProductByIdStorage.GetFinishedProductById(getOrdersByFinishedProductQuery.FinishedProductId, cancellationToken);
        
        if (finishedProductExists is null)
            throw new NotFoundByIdException(getOrdersByFinishedProductQuery.FinishedProductId, "Готовое мебельное изделие");

        return await storage.GetFinishedProductOrders(getOrdersByFinishedProductQuery.FinishedProductId,
            getOrdersByFinishedProductQuery.CreatedAt, getOrdersByFinishedProductQuery.CompletedAt,
            getOrdersByFinishedProductQuery.Status, getOrdersByFinishedProductQuery.Description, cancellationToken);
    }
}