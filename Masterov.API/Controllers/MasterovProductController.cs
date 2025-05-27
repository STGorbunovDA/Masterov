using AutoMapper;
using Masterov.API.Models;
using Masterov.Domain.Masterov.Product.GetProductById;
using Masterov.Domain.Masterov.Product.GetProductById.Query;
using Masterov.Domain.Masterov.Product.GetProducts;
using Microsoft.AspNetCore.Mvc;

namespace Masterov.API.Controllers;

[ApiController]
[Route("api/MasterovProduct")]
public class MasterovProductController(IMapper mapper): ControllerBase
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
    
    /// <summary>
    /// Получить изделие по Id
    /// </summary>
    /// <param name="productId">Идентификатор изделия</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация об изделии</returns>
    [HttpGet("getProductById/{productId:guid}")] // Исправлено: используется стандартный constraint 'guid'
    [ProducesResponseType(200, Type = typeof(ProductRequest))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetProductById(
        [FromRoute] Guid productId,
        [FromServices] IGetProductByIdUseCase useCase,
        CancellationToken cancellationToken)
    {
        var product = await useCase.Execute(new GetProductByIdQuery(productId), cancellationToken);
        return Ok(mapper.Map<ProductRequest>(product));
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