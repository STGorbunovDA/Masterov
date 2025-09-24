using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Supply.DeleteSupply.Command;
using Masterov.Domain.Masterov.Supply.GetSupplyById;

namespace Masterov.Domain.Masterov.Supply.DeleteSupply;

public class DeleteSupplyUseCase(IValidator<DeleteSupplyCommand> validator, 
    IDeleteSupplyStorage storage, IGetSupplyByIdStorage getSupplyByIdStorage) : IDeleteSupplyUseCase
{
    public async Task<bool> Execute(DeleteSupplyCommand deleteSupplyCommand, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(deleteSupplyCommand, cancellationToken);
        
        var supplyExists = await getSupplyByIdStorage.GetSupplyById(deleteSupplyCommand.SupplyId, cancellationToken);
        
        if (supplyExists is null)
            throw new NotFoundByIdException(deleteSupplyCommand.SupplyId, "Поставка");
        
        return await storage.DeleteSupply(deleteSupplyCommand.SupplyId, cancellationToken);
    }
}