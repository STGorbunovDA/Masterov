using AutoMapper;
using Masterov.Domain.Masterov.FinishedProduct.UpdateFinishedProduct;
using Masterov.Domain.Models;

namespace Masterov.Storage.Storages.Masterov.FinishedProduct;

internal class UpdateFinishedProductStorage(MasterovDbContext dbContext, IMapper mapper) : IUpdateFinishedProductStorage
{
    public async Task<FinishedProductDomain> UpdateFinishedProduct(Guid finishedProductId, string name, decimal? price, int? width, int? height, int? depth,
        byte[]? image, CancellationToken cancellationToken)
    {
        var finishedProductExists = await dbContext.Set<Storage.FinishedProduct>().FindAsync([finishedProductId], cancellationToken);
        
        if (finishedProductExists == null)
            throw new Exception("FinishedProduct not found");
        
        finishedProductExists.Name = name;
        finishedProductExists.UploadedAt = DateTime.UtcNow;
        finishedProductExists.Price = price ?? finishedProductExists.Price;
        finishedProductExists.Width = width ?? finishedProductExists.Width;
        finishedProductExists.Height = height ?? finishedProductExists.Height;
        finishedProductExists.Depth = depth ?? finishedProductExists.Depth;
        finishedProductExists.Image = image ?? finishedProductExists.Image;
        
        await dbContext.SaveChangesAsync(cancellationToken);

        return mapper.Map<FinishedProductDomain>(finishedProductExists);
    }
}