using System.Net.Http.Json;
using Masterov.Web.Models;

namespace Masterov.Web.Services.FinishedProduct;

public class FinishedProductService(HttpClient http, ILogger<FinishedProductService> logger) : IFinishedProductService
{
    public async Task<IEnumerable<FinishedProductDto>> GetEliteProductsWithoutOrdersAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await http.GetFromJsonAsync<IEnumerable<FinishedProductDto>>(
                "api/finishedProducts/getFinishedProductsByEliteWithoutOrders", cancellationToken);
            if(result is not null) FinishedProducts = result;
            return FinishedProducts;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении элитных готовых мебельных изделий");
            return Enumerable.Empty<FinishedProductDto>();
        }
    }

    public IEnumerable<FinishedProductDto> FinishedProducts { get; set; } = new List<FinishedProductDto>();
   
}