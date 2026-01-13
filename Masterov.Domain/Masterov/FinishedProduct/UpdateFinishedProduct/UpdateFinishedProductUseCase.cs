using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductById;
using Masterov.Domain.Masterov.FinishedProduct.UpdateFinishedProduct.Command;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.FinishedProduct.UpdateFinishedProduct;

public class UpdateFinishedProductUseCase(IValidator<UpdateFinishedProductCommand> validator,
    IUpdateFinishedProductStorage updateFinishedProductStorage, IGetFinishedProductByIdStorage getFinishedProductByIdStorage) : IUpdateFinishedProductUseCase
{
    public async Task<FinishedProductDomain> Execute(UpdateFinishedProductCommand updateFinishedProductCommand,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(updateFinishedProductCommand, cancellationToken);
        
        var finishedProductExists = await getFinishedProductByIdStorage.GetFinishedProductById(updateFinishedProductCommand.FinishedProductId, cancellationToken);

        if (finishedProductExists is null)
            throw new NotFoundByIdException(updateFinishedProductCommand.FinishedProductId, "Готовое мебельное изделие");
        
        return await updateFinishedProductStorage.UpdateFinishedProduct(updateFinishedProductCommand.FinishedProductId, 
            updateFinishedProductCommand.Name, updateFinishedProductCommand.Type, updateFinishedProductCommand.Price, updateFinishedProductCommand.Width, 
            updateFinishedProductCommand.Height, updateFinishedProductCommand.Depth, updateFinishedProductCommand.Image, 
            updateFinishedProductCommand.CreatedAt, updateFinishedProductCommand.Elite, cancellationToken);
    }
}