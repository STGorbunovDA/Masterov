using AutoMapper;
using Masterov.Domain.Masterov.ProductType.UpdateProductType;
using Masterov.Domain.Models;

namespace Masterov.Storage.Storages.Masterov.ProductType;

internal class UpdateProductTypeStorage(MasterovDbContext dbContext, IMapper mapper) : IUpdateProductTypeStorage
{
    public async Task<ProductTypeDomain> UpdateProductType(Guid productTypeId, string name, string? description, CancellationToken cancellationToken)
    {
        var productType = await dbContext.Set<Storage.ProductType>().FindAsync([productTypeId], cancellationToken);
        
        productType!.Name = name; 
        productType.Description = description;

        await dbContext.SaveChangesAsync(cancellationToken);

        return mapper.Map<ProductTypeDomain>(productType);
    }
}