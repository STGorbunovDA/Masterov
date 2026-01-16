using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.ProductType.GetProductTypes;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.ProductType;

internal class GetProductTypesStorage(MasterovDbContext dbContext, IMapper mapper) : IGetProductTypesStorage
{
    public async Task<IEnumerable<ProductTypeDomain?>> GetProductTypes(CancellationToken cancellationToken)
    {
        return await dbContext.ProductTypes
            .AsNoTracking()
            .ProjectTo<ProductTypeDomain>(mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);
    }
}