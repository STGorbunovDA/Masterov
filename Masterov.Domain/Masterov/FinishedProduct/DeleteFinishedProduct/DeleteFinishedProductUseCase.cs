using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.FinishedProduct.DeleteFinishedProduct.Command;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductById;

namespace Masterov.Domain.Masterov.FinishedProduct.DeleteFinishedProduct;

public class DeleteFinishedProductUseCase(IValidator<DeleteFinishedProductCommand> validator, 
    IDeleteFinishedProductStorage storage, IGetFinishedProductByIdStorage getFinishedProductByIdStorage) : IDeleteFinishedProductUseCase
{
    public async Task<bool> Execute(DeleteFinishedProductCommand deleteFinishedProductCommand, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(deleteFinishedProductCommand, cancellationToken);
        
        var finishedProductExists = await getFinishedProductByIdStorage.GetFinishedProductById(deleteFinishedProductCommand.FinishedProductId, cancellationToken);
        
        if (finishedProductExists is null)
            throw new NotFoundByIdException(deleteFinishedProductCommand.FinishedProductId, "Готовое мебельное изделие");
        
        return await storage.DeleteFinishedProduct(deleteFinishedProductCommand.FinishedProductId, cancellationToken);
    }
}