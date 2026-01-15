using AutoMapper;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByDescription;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.FinishedProduct;

public class GetFinishedProductsByDescriptionStorage (MasterovDbContext dbContext, IMapper mapper) : IGetFinishedProductsByDescriptionStorage
{
    public async Task<IEnumerable<FinishedProductDomain>?> GetFinishedProductsByDescription(string description, CancellationToken cancellationToken)
    {
        var finishedProducts = await dbContext.FinishedProducts
            .AsNoTracking() 
            .Where(f => f.Description.Contains(description))
            .ToArrayAsync(cancellationToken);

        return mapper.Map<IEnumerable<FinishedProductDomain>>(finishedProducts);
    }
}