using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.ProductType.GetProductTypeById;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.ProductType;

internal class GetProductTypeByIdStorage(MasterovDbContext dbContext, IMapper mapper) : IGetProductTypeByIdStorage
{
    public async Task<ProductTypeDomain?> GetProductTypeById(Guid productTypeId, CancellationToken cancellationToken) =>
        await dbContext.ProductTypes
            .AsNoTracking() 
            .Where(f => f.ProductTypeId == productTypeId)
            .ProjectTo<ProductTypeDomain>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
}