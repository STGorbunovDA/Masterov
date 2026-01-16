using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.FinishedProduct.AddFinishedProduct;
using Masterov.Domain.Models;
using Masterov.Storage.Extension;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.FinishedProduct;

internal class AddFinishedProductStorage(MasterovDbContext dbContext, IGuidFactory guidFactory, IMapper mapper) : IAddFinishedProductStorage
{
    public async Task<FinishedProductDomain> AddFinishedProduct(string name, string type, decimal? price, int? width, int? height, int? depth, 
        byte[]? image, bool elite, string description,
        CancellationToken cancellationToken)
    {
       
        var normalizedType = type.Trim().ToLower();
        var productType = await dbContext.ProductTypes
            .FirstOrDefaultAsync(t => t.Name.ToLower() == normalizedType, cancellationToken);

        if (productType == null)
        {
            productType = new Storage.ProductType
            {
                ProductTypeId = guidFactory.Create(),
                Name = type.Trim(),
                CreatedAt = DateTime.Now
            };

            await dbContext.ProductTypes.AddAsync(productType, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        var finishedProductGuide = guidFactory.Create();
        var finishedProduct = new Storage.FinishedProduct
        {
            FinishedProductId = finishedProductGuide,
            Name = name,
            ProductTypeId = productType.ProductTypeId,
            Price = price ?? 0.00m,
            Width = width ?? 0,
            Height = height ?? 0,
            Depth = depth ?? 0,
            Image = image,
            CreatedAt = DateTime.Now,
            Elite = elite,
            Description = description
        };
        
        await dbContext.FinishedProducts.AddAsync(finishedProduct, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return await dbContext.FinishedProducts
            .Where(t => t.FinishedProductId == finishedProductGuide)
            .ProjectTo<FinishedProductDomain>(mapper.ConfigurationProvider)
            .FirstAsync(cancellationToken);
    }
}