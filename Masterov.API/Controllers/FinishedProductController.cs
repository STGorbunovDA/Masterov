using AutoMapper;
using Masterov.API.Extensions;
using Masterov.API.Models.FinishedProduct;
using Masterov.Domain.Masterov.FinishedProduct.AddFinishedProduct;
using Masterov.Domain.Masterov.FinishedProduct.AddFinishedProduct.Command;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductById;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductById.Query;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductByName;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductByName.Query;
using Masterov.Domain.Masterov.FinishedProduct.GetProducts;
using Microsoft.AspNetCore.Mvc;

namespace Masterov.API.Controllers;

[ApiController]
[Route("api/finishedProduct")]
public class FinishedProductController(IMapper mapper): ControllerBase
{
    /// <summary>
    /// Получить все готовые мебельные изделия
    /// </summary>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о всех готовых мебельных изделий</returns>
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
    /// Получить готовое мебельное изделие по Id
    /// </summary>
    /// <param name="finishedProductId">Идентификатор изделия</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о готовом мебельном изделии</returns>
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
    
    /// <summary>
    /// Получить готовое мебельное изделие по имени
    /// </summary>
    /// <param name="finishedProductName">Название готового мебельного изделия</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о готовом мебельном изделии</returns>
    [HttpGet("getFinishedProductByName/{finishedProductName}")]
    [ProducesResponseType(200, Type = typeof(FinishedProductRequest))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetFinishedProductByName(
        [FromRoute] string finishedProductName,
        [FromServices] IGetFinishedProductByNameUseCase useCase,
        CancellationToken cancellationToken)
    {
        var productType = await useCase.Execute(new GetFinishedProductByNameQuery(finishedProductName), cancellationToken);
        return Ok(mapper.Map<FinishedProductRequest>(productType));
    }
    
    /// <summary>
    /// Добавить готовое мебельное изделие
    /// </summary>
    [HttpPost("addFinishedProduct")]
    [ProducesResponseType(201, Type = typeof(FinishedProductRequest))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(410)]
    public async Task<IActionResult> AddFinishedProduct(
        [FromForm] AddFinishedProductRequest request,
        [FromServices] IAddFinishedProductUseCase useCase,
        CancellationToken cancellationToken)
    {
        if (request.Image is { Length: 0 })
            return BadRequest("Изображение изделия не загружено или пустое изображение");
        
        if (request.Image is { Length: > 100 * 1024 * 1024 } )
            return BadRequest("Изображение должно быть не более 100 мб.");
    
        var finishedProduct = await useCase.Execute(
            new AddFinishedProductCommand(
                request.Name, 
                request.Price, 
                request.Width, 
                request.Height, 
                request.Depth, 
                request.Image == null ? null : await request.Image.ToByteArrayAsync()),
            cancellationToken);

        return Ok(finishedProduct);
        //return CreatedAtAction(nameof(GetFileById), new { id = artifactDomain.ArtifactId }, mapper.Map<ArtifactDto>(artifactDomain));
    }
}