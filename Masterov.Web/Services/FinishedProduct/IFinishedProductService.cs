using Masterov.Web.Models;

namespace Masterov.Web.Services.FinishedProduct;

public interface IFinishedProductService
{
    IEnumerable<FinishedProductDto> FinishedProducts { get; set; }
    Task<IEnumerable<FinishedProductDto>> GetEliteProductsWithoutOrdersAsync(CancellationToken cancellationToken = default);
}