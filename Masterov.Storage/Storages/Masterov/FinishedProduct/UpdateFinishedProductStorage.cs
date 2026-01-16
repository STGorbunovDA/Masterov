using AutoMapper;
using Masterov.Domain.Masterov.FinishedProduct.UpdateFinishedProduct;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.FinishedProduct;

internal class UpdateFinishedProductStorage(MasterovDbContext dbContext, IMapper mapper) : IUpdateFinishedProductStorage
{
    public async Task<FinishedProductDomain> UpdateFinishedProduct(Guid finishedProductId, string name, string type, decimal? price, int? width, int? height, int? depth,
        byte[]? image, DateTime? createdAt, bool elite, string description, CancellationToken cancellationToken)
    {
        var finishedProductExists = await dbContext.Set<Storage.FinishedProduct>().FindAsync([finishedProductId], cancellationToken);
        
        if (finishedProductExists == null)
            throw new Exception("FinishedProduct not found");
        
        // 1. Поиск типа
        var normalizedType = type.Trim().ToLower();

        var productType = await dbContext.ProductTypes
            .FirstOrDefaultAsync(t => t.Name.ToLower() == normalizedType, cancellationToken);

        // 2. Если типа нет — создаём
        if (productType == null)
        {
            productType = new Storage.ProductType
            {
                Name = type.Trim()
            };

            await dbContext.ProductTypes.AddAsync(productType, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        
        finishedProductExists.Name = name.Trim();
        finishedProductExists.ProductTypeId = productType.Id;

        if (createdAt.HasValue)
            finishedProductExists.CreatedAt = createdAt.Value;
        
        finishedProductExists.UpdatedAt = DateTime.Now;
        finishedProductExists.Price = price ?? finishedProductExists.Price;
        finishedProductExists.Width = width ?? finishedProductExists.Width;
        finishedProductExists.Height = height ?? finishedProductExists.Height;
        finishedProductExists.Depth = depth ?? finishedProductExists.Depth;
        finishedProductExists.Image = image ?? finishedProductExists.Image;
        finishedProductExists.Elite = elite;
        if(!string.IsNullOrWhiteSpace(description))
            finishedProductExists.Description = description;
        await dbContext.SaveChangesAsync(cancellationToken);

        return mapper.Map<FinishedProductDomain>(finishedProductExists);
    }
}