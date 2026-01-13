using System.Net.Http.Json;
using Masterov.Web.Models;

namespace Masterov.Web.Services.FinishedProduct;

public class FinishedProductService(HttpClient http, ILogger<FinishedProductService> logger)
{
    public async Task<IEnumerable<FinishedProductDto>> GetEliteProductsWithoutOrdersAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await http.GetFromJsonAsync<IEnumerable<FinishedProductDto>>(
                "getFinishedProductsByEliteWithoutOrders", cancellationToken);

            return result ?? Enumerable.Empty<FinishedProductDto>();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении элитных изделий");
            return Enumerable.Empty<FinishedProductDto>();
        }
    }
}