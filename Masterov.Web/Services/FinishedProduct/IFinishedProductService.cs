using Masterov.Web.Models;

namespace Masterov.Web.Services.FinishedProduct;

public interface IFinishedProductService
{
    IEnumerable<FinishedProductDto> FinishedProducts { get; set; }
    Task<IEnumerable<FinishedProductDto>> GetEliteFinishedProductsWithoutOrders(CancellationToken cancellationToken = default);
    Task<FinishedProductDto> GetFinishedProductByIdWithoutOrders(Guid productId, CancellationToken cancellationToken = default);
    Task<IEnumerable<FinishedProductDto>> GetFinishedProductsByTypeWithoutOrders(string type, CancellationToken cancellationToken = default);
}