using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.ProductType.AddProductType;
using Masterov.Domain.Models;
using Masterov.Storage.Extension;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.ProductType;

internal class AddProductTypeStorage(MasterovDbContext dbContext, IGuidFactory guidFactory, IMapper mapper) : IAddProductTypeStorage
{
    public async Task<ProductTypeDomain> AddProductType(string name, CancellationToken cancellationToken)
    {
        var productTypeGuide = guidFactory.Create();
        var productType = new Storage.ProductType
        {
            ProductTypeId = productTypeGuide,
            Name = name,
            CreatedAt = DateTime.Now,
        };

        await dbContext.ProductTypes.AddAsync(productType, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return await dbContext.ProductTypes
            .Where(t => t.ProductTypeId == productTypeGuide)
            .ProjectTo<ProductTypeDomain>(mapper.ConfigurationProvider)
            .FirstAsync(cancellationToken);
    }
}