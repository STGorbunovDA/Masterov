using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.ProductType.GetProductTypeById;
using Masterov.Domain.Masterov.ProductType.UpdateProductType.Command;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductType.UpdateProductType;

public class UpdateProductTypeUseCase(IValidator<UpdateProductTypeCommand> validator,
    IUpdateProductTypeStorage updateProductTypeStorage, IGetProductTypeByIdStorage getProductTypeByIdStorage) : IUpdateProductTypeUseCase
{
    public async Task<ProductTypeDomain> Execute(UpdateProductTypeCommand updateProductTypeCommand,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(updateProductTypeCommand, cancellationToken);
        
        var productTypeExists = await getProductTypeByIdStorage.GetProductTypeById(updateProductTypeCommand.ProductTypeId, cancellationToken);

        if (productTypeExists is null)
            throw new NotFoundByIdException(updateProductTypeCommand.ProductTypeId, "Тип изделия");

        return await updateProductTypeStorage.UpdateProductType(updateProductTypeCommand.ProductTypeId, updateProductTypeCommand.Name, updateProductTypeCommand.Description, cancellationToken);
    }
}