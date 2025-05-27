using AutoMapper;
using Masterov.API.Models;
using Masterov.Domain.Masterov.Product.GetProductById;
using Masterov.Domain.Masterov.Product.GetProductById.Query;
using Masterov.Domain.Masterov.Product.GetProducts;
using Masterov.Domain.Masterov.ProductType.GetProductsType;
using Masterov.Domain.Masterov.ProductType.GetProductTypeById;
using Masterov.Domain.Masterov.ProductType.GetProductTypeById.Query;
using Microsoft.AspNetCore.Mvc;

namespace Masterov.API.Controllers;

[ApiController]
[Route("api/MasterovProductType")]
public class MasterovProductTypeController(IMapper mapper): ControllerBase
{
    /// <summary>
    /// Получить все типы изделий
    /// </summary>
    /// <param name="useCase"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Типы изделий</returns>
    [HttpGet("getProductsType")]
    [ProducesResponseType(200, Type = typeof(ProductTypeRequest[]))]
    [ProducesResponseType(410)]
    public async Task<IActionResult> GetProductsType(
        [FromServices] IGetProductsTypeUseCase useCase,
        CancellationToken cancellationToken)
    {
        var files = await useCase.Execute(cancellationToken);
        return Ok(files.Select(mapper.Map<ProductTypeRequest>));
    }
    
    /// <summary>
    /// Получить изделие по Id
    /// </summary>
    /// <param name="productTypeId">Идентификатор изделия</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о типе изделия</returns>
    [HttpGet("getProductTypeById/{productTypeId:guid}")]
    [ProducesResponseType(200, Type = typeof(ProductTypeRequest))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetProductTypeById(
        [FromRoute] Guid productTypeId,
        [FromServices] IGetProductTypeByIdUseCase useCase,
        CancellationToken cancellationToken)
    {
        var product = await useCase.Execute(new GetProductTypeByIdQuery(productTypeId), cancellationToken);
        return Ok(mapper.Map<ProductTypeRequest>(product));
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