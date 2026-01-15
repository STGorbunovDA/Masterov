using AutoMapper;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByDescriptionAtWithoutOrders;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.FinishedProduct;

public class GetFinishedProductsByDescriptionAtWithoutOrdersStorage (MasterovDbContext dbContext, IMapper mapper) : IGetFinishedProductsByDescriptionAtWithoutOrdersStorage
{
    public async Task<IEnumerable<FinishedProductNoOrdersDomain>?> GetFinishedProductsByDescriptionAtWithoutOrders(string description, CancellationToken cancellationToken)
    {
        var finishedProducts = await dbContext.FinishedProducts
            .AsNoTracking() 
            .Where(f => f.Description.Contains(description))
            .ToArrayAsync(cancellationToken);

        return mapper.Map<IEnumerable<FinishedProductNoOrdersDomain>>(finishedProducts);
    }
}