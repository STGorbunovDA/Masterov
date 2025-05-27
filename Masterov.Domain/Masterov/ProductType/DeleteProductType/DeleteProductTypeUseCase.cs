using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.ProductType.DeleteProductType.Command;
using Masterov.Domain.Masterov.ProductType.GetProductTypeById;

namespace Masterov.Domain.Masterov.ProductType.DeleteProductType;

public class DeleteProductTypeUseCase(IValidator<DeleteProductTypeCommand> validator, 
    IGetProductTypeByIdStorage getProductTypeByIdStorage, 
    IDeleteProductTypeStorage storage) : IDeleteProductTypeUseCase
{
    public async Task<bool> Execute(DeleteProductTypeCommand deleteProductTypeCommand, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(deleteProductTypeCommand, cancellationToken);
        
        var productTypeExists = await getProductTypeByIdStorage.GetProductTypeById(deleteProductTypeCommand.productTypeId, cancellationToken);
        if (productTypeExists is null)
            throw new NotFoundByIdException(deleteProductTypeCommand.productTypeId, "Тип изделия");
        
        return await storage.DeleteProductType(deleteProductTypeCommand.productTypeId, cancellationToken);
    }
}