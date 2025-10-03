using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.FinishedProduct.UpdateFinishedProduct;

public interface IUpdateFinishedProductStorage
{
    Task<FinishedProductDomain> UpdateFinishedProduct(Guid finishedProductId, string name, decimal? price, int? width, int? height, int? depth, byte[]? image, DateTime? createdAt, CancellationToken cancellationToken);
}