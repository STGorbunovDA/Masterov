using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.FinishedProduct.AddFinishedProduct;

public interface IAddFinishedProductStorage
{
    Task<FinishedProductDomain> AddFinishedProduct(string name, string type, decimal? price, int? width, int? height, int? depth, byte[]? image, bool elite, CancellationToken cancellationToken);
}