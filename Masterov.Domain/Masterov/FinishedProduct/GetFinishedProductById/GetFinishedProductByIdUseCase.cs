using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductById.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductById;

public class GetFinishedProductByIdUseCase(IValidator<GetFinishedProductByIdQuery> validator, IGetFinishedProductByIdStorage storage) : IGetFinishedProductByIdUseCase
{
    public async Task<FinishedProductDomain?> Execute(GetFinishedProductByIdQuery getFinishedProductByIdQuery, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(getFinishedProductByIdQuery, cancellationToken);
        var finishedProductExists = await storage.GetFinishedProductById(getFinishedProductByIdQuery.FinishedProductId, cancellationToken);
        
        if (finishedProductExists is null)
            throw new NotFoundByIdException(getFinishedProductByIdQuery.FinishedProductId, "Изделие");
        
        return finishedProductExists;
    }
}