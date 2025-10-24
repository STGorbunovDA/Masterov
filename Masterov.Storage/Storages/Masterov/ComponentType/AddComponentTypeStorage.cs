using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.ComponentType.AddComponentType;
using Masterov.Domain.Models;
using Masterov.Storage.Extension;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.ComponentType;

internal class AddComponentTypeStorage(MasterovDbContext dbContext, IGuidFactory guidFactory, IMapper mapper) : IAddComponentTypeStorage
{
    public async Task<ComponentTypeDomain> AddComponentType(string name, string? description, CancellationToken cancellationToken)
    {
        var productTypeGuide = guidFactory.Create();
        var productType = new Storage.ComponentType
        {
            ComponentTypeId = productTypeGuide,
            Name = name,
            Description = description
        };

        await dbContext.ComponentTypes.AddAsync(productType, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return await dbContext.ComponentTypes
            .Where(t => t.ComponentTypeId == productTypeGuide)
            .ProjectTo<ComponentTypeDomain>(mapper.ConfigurationProvider)
            .FirstAsync(cancellationToken);
    }
}