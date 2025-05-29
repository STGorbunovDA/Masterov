using AutoMapper;
using Masterov.API.Models;
using Masterov.API.Models.FinishedProduct;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductById;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductById.Query;
using Masterov.Domain.Masterov.FinishedProduct.GetProducts;
using Microsoft.AspNetCore.Mvc;

namespace Masterov.API.Controllers;

[ApiController]
[Route("api/finishedProduct")]
public class FinishedProductController(IMapper mapper): ControllerBase
{
    /// <summary>
    /// Получить все изделия
    /// </summary>
    /// <param name="useCase"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("getFinishedProducts")]
    [ProducesResponseType(200, Type = typeof(FinishedProductRequest[]))]
    [ProducesResponseType(410)]
    public async Task<IActionResult> GetFinishedProducts(
        [FromServices] IGetFinishedProductsUseCase useCase,
        CancellationToken cancellationToken)
    {
        var files = await useCase.Execute(cancellationToken);
        return Ok(files.Select(mapper.Map<FinishedProductRequest>));
    }

    /// <summary>
    /// Получить изделие по Id
    /// </summary>
    /// <param name="finishedProductId">Идентификатор изделия</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация об изделии</returns>
    [HttpGet("getFinishedProductById/{finishedProductId:guid}")]
    [ProducesResponseType(200, Type = typeof(FinishedProductRequest))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetFinishedProductById(
        [FromRoute] Guid finishedProductId,
        [FromServices] IGetFinishedProductByIdUseCase useCase,
        CancellationToken cancellationToken)
    {
        var product = await useCase.Execute(new GetFinishedProductByIdQuery(finishedProductId), cancellationToken);
        return Ok(mapper.Map<FinishedProductRequest>(product));
    }
    
    // /// <summary>
    // /// Добавить изделие
    // /// </summary>
    // [HttpPost("addProduct")]
    // [ProducesResponseType(201, Type = typeof(ProductRequest))]
    // [ProducesResponseType(400, Type = typeof(string))]
    // [ProducesResponseType(410)]
    // public async Task<IActionResult> AddProduct(
    //     [FromForm] AddProductRequest request,
    //     [FromServices] IAddProductUseCase useCase,
    //     CancellationToken cancellationToken)
    // {
    //     if (request.Content is { Length: 0 })
    //         return BadRequest("Изображение изделия не загружено или пустое изображение");
    //
    //     var product = await useCase.Execute(
    //         new AddProductCommand(fileName, type, await request.File.ToByteArrayAsync()),
    //         cancellationToken);
    //
    //     return CreatedAtAction(nameof(GetFileById), new { id = artifactDomain.ArtifactId }, mapper.Map<ArtifactDto>(artifactDomain));
    // }
}