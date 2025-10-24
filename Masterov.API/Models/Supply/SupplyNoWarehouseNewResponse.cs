﻿using Masterov.API.Models.ComponentType;
using Masterov.API.Models.Supplier;

namespace Masterov.API.Models.Supply;

public class SupplyNoWarehouseNewResponse
{
    public Guid SupplyId { get; set; }
    public SupplierRequestNoSupply Supplier { get; set; }
    public ComponentTypeResponse ComponentType { get; set; }
    public int Quantity { get; set; }
    public decimal PriceSupply { get; set; }
    public DateTime SupplyDate { get; set; }
}