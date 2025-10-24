using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.ComponentType.DeleteComponentType.Command;
using Masterov.Domain.Masterov.ComponentType.GetComponentTypeById;

namespace Masterov.Domain.Masterov.ComponentType.DeleteComponentType;

public class DeleteComponentTypeUseCase(IValidator<DeleteComponentTypeCommand> validator, 
    IGetComponentTypeByIdStorage getComponentTypeByIdStorage, 
    IDeleteComponentTypeStorage storage) : IDeleteComponentTypeUseCase
{
    public async Task<bool> Execute(DeleteComponentTypeCommand deleteComponentTypeCommand, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(deleteComponentTypeCommand, cancellationToken);
        
        var componentTypeExists = await getComponentTypeByIdStorage.GetComponentTypeById(deleteComponentTypeCommand.ComponentTypeId, cancellationToken);
        if (componentTypeExists is null)
            throw new NotFoundByIdException(deleteComponentTypeCommand.ComponentTypeId, "Тип изделия");
        
        return await storage.DeleteComponentType(deleteComponentTypeCommand.ComponentTypeId, cancellationToken);
    }
}