using AutoMapper;
using Masterov.API.Extensions;
using Masterov.API.Models;
using Masterov.Domain.Masterov.GetProducts;
using Microsoft.AspNetCore.Mvc;

namespace Masterov.API.Controllers;

[ApiController]
[Route("api/masterov")]
public class MasterovController(IMapper mapper): ControllerBase
{
    /// <summary>
    /// Получить все изделия
    /// </summary>
    /// <param name="useCase"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("getProducts")]
    [ProducesResponseType(200, Type = typeof(ProductRequest[]))]
    [ProducesResponseType(410)]
    public async Task<IActionResult> GetProducts(
        [FromServices] IGetProductsUseCase useCase,
        CancellationToken cancellationToken)
    {
        var files = await useCase.Execute(cancellationToken);
        return Ok(files.Select(mapper.Map<ProductRequest>));
    }
}