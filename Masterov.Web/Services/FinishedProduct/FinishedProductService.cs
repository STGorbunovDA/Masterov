using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Masterov.Web.Models;

namespace Masterov.Web.Services.FinishedProduct;

public class FinishedProductService(HttpClient http, ILogger<FinishedProductService> logger) 
    : IFinishedProductService
{
    public async Task<IEnumerable<FinishedProductDto>> GetEliteFinishedProductsWithoutOrders(
        CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await http.GetAsync(
                "api/finishedProducts/getFinishedProductsByEliteWithoutOrders",
                cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                logger.LogError(
                    "Ошибка API при получении элитных изделий: {StatusCode}",
                    response.StatusCode);
                return Enumerable.Empty<FinishedProductDto>();
            }

            var result = await response.Content.ReadFromJsonAsync<IEnumerable<FinishedProductDto>>(
                cancellationToken);

            if (result is not null)
            {
                FinishedProducts = result;
                return result;
            }

            logger.LogWarning("API вернул пустой список элитных изделий");
            return Enumerable.Empty<FinishedProductDto>();
        }
        catch (HttpRequestException httpEx)
        {
            logger.LogError(httpEx, "Ошибка HTTP при получении элитных изделий");
            return Enumerable.Empty<FinishedProductDto>();
        }
        catch (JsonException jsonEx)
        {
            logger.LogError(jsonEx, "Ошибка десериализации списка элитных изделий");
            return Enumerable.Empty<FinishedProductDto>();
        }
        catch (TaskCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            logger.LogWarning("Запрос элитных изделий был отменен");
            return Enumerable.Empty<FinishedProductDto>();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Неожиданная ошибка при получении элитных изделий");
            return Enumerable.Empty<FinishedProductDto>();
        }
    }

    public async Task<FinishedProductDto?> GetFinishedProductByIdWithoutOrders(
        Guid productId, 
        CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await http.GetAsync(
                $"api/finishedProducts/getFinishedProductByIdWithoutOrders/{productId}",
                cancellationToken);

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                logger.LogWarning("Продукт с ID {Id} не найден", productId);
                return null;
            }

            if (!response.IsSuccessStatusCode)
            {
                logger.LogError("Ошибка API при получении изделия по ID {Id}: {StatusCode}", 
                    productId, response.StatusCode);
                return null;
            }

            var result = await response.Content.ReadFromJsonAsync<FinishedProductDto>(
                cancellationToken);

            return result;
        }
        catch (HttpRequestException httpEx)
        {
            logger.LogError(httpEx, "Ошибка HTTP при получении изделия по ID {Id}", productId);
            return null;
        }
        catch (JsonException jsonEx)
        {
            logger.LogError(jsonEx, "Ошибка десериализации ответа для изделия с ID {Id}", productId);
            return null;
        }
        catch (TaskCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            logger.LogWarning("Запрос изделия по ID {Id} был отменен", productId);
            return null;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Неожиданная ошибка при получении изделия по ID {Id}", productId);
            return null;
        }
    }

    public async Task<IEnumerable<FinishedProductDto>> GetFinishedProductsByTypeWithoutOrders(string type, CancellationToken cancellationToken = default)
    {
        try
        {
            var encodedType = WebUtility.UrlEncode(type);
        
            var response = await http.GetAsync(
                $"api/finishedProducts/getFinishedProductsByTypeWithoutOrders?FinishedProductType={encodedType}",
                cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                logger.LogError(
                    "Ошибка API при получении изделий по типу '{Type}': {StatusCode}",
                    type, response.StatusCode);
                return Enumerable.Empty<FinishedProductDto>();
            }

            var result = await response.Content.ReadFromJsonAsync<IEnumerable<FinishedProductDto>>(
                cancellationToken);

            if (result is not null)
            {
                logger.LogInformation(
                    "Получено {Count} изделий типа '{Type}'",
                    result.Count(), type);
                FinishedProducts = result;
                return result;
            }

            logger.LogWarning("API вернул пустой список изделий для типа '{Type}'", type);
            return Enumerable.Empty<FinishedProductDto>();
        }
        catch (HttpRequestException httpEx)
        {
            logger.LogError(httpEx, "Ошибка HTTP при получении изделий по типу '{Type}'", type);
            return Enumerable.Empty<FinishedProductDto>();
        }
        catch (JsonException jsonEx)
        {
            logger.LogError(jsonEx, "Ошибка десериализации списка изделий для типа '{Type}'", type);
            return Enumerable.Empty<FinishedProductDto>();
        }
        catch (TaskCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            logger.LogWarning("Запрос изделий по типу '{Type}' был отменен", type);
            return Enumerable.Empty<FinishedProductDto>();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Неожиданная ошибка при получении изделий по типу '{Type}'", type);
            return Enumerable.Empty<FinishedProductDto>();
        }
    }

    public IEnumerable<FinishedProductDto> FinishedProducts { get; set; } = new List<FinishedProductDto>();
}