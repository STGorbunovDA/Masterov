﻿namespace Masterov.API.Models.FinishedProduct;

public class UpdateFinishedProductRequest
{
    public Guid FinishedProductId { get; set; }
    public string Name { get; set; }
    public decimal? Price { get; set; }
    public int? Width { get; set; }  // в мм
    public int? Height { get; set; }  // в мм
    public int? Depth { get; set; }  // в мм
    public IFormFile? Image { get; set; }
}