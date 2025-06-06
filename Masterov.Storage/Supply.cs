﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Masterov.Storage;

public class Supply
{
    [Key]
    public Guid SupplyId { get; set; } = Guid.NewGuid();

    [Required, ForeignKey(nameof(Supplier))]
    public Guid SupplierId { get; set; }
    public Supplier Supplier { get; set; }

    [Required, ForeignKey(nameof(ProductType))]
    public Guid ProductTypeId { get; set; }
    public ProductType ProductType { get; set; }

    [Required, ForeignKey(nameof(Warehouse))]
    public Guid WarehouseId { get; set; }
    public Warehouse Warehouse { get; set; }

    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal PriceSupply { get; set; }

    public DateTime SupplyDate { get; set; } = DateTime.UtcNow;
}