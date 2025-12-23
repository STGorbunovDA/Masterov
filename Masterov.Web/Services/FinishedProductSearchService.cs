using System.Net.Http.Json;
using Masterov.Web.Models.FinishedProduct;
using Newtonsoft.Json;

namespace Masterov.Web.Services;

public class FinishedProductSearchService(HttpClient http, ILogger<FinishedProductSearchService> logger)
{
    private async Task<IEnumerable<FinishedProductDto>> SearchAsync(string endpoint, string paramName, string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Array.Empty<FinishedProductDto>();

        try
        {
            var encoded = Uri.EscapeDataString(value);
            var url = $"{endpoint}?{paramName}={encoded}";

            logger.LogInformation("Search request to: {Url}", url);

            var result = await http.GetFromJsonAsync<IEnumerable<FinishedProductDto>>(url);

            return result ?? Array.Empty<FinishedProductDto>();
        }
        catch (Exception ex) when (
            ex is HttpRequestException or NotSupportedException or JsonException)
        {
            logger.LogError(ex, "Search error");
            return Array.Empty<FinishedProductDto>();
        }
    }
    
    public Task<IEnumerable<FinishedProductDto>> SearchByName(string name)
        => SearchAsync("api/finishedProducts/getFinishedProductsByNameWithoutOrders", 
            "FinishedProductName", name);

    public Task<IEnumerable<FinishedProductDto>> SearchByType(string type)
        => SearchAsync("api/finishedProducts/getFinishedProductsByTypeWithoutOrders", 
            "FinishedProductType", type);
}