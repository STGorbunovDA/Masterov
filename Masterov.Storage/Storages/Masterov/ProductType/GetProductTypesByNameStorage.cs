using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.ProductType.GetProductTypesByName;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.ProductType;

internal class GetProductTypesByNameStorage(MasterovDbContext dbContext, IMapper mapper) : IGetProductTypesByNameStorage
{
    public async Task<IEnumerable<ProductTypeDomain?>> GetProductTypesByName(string productTypeName, CancellationToken cancellationToken)
    {
        var normalizedName = productTypeName.Trim().ToLower();

        return await dbContext.ProductTypes
            .AsNoTracking()
            .Where(f => f.Name.ToLower().Contains(normalizedName))
            .ProjectTo<ProductTypeDomain>(mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);
    }
}