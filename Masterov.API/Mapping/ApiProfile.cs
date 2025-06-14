﻿using AutoMapper;
using Masterov.API.Models;
using Masterov.API.Models.Auth;
using Masterov.API.Models.FinishedProduct;
using Masterov.API.Models.ProductionOrder;
using Masterov.API.Models.ProductType;
using Masterov.Domain.Models;

namespace Masterov.API.Mapping;

internal class ApiProfile : Profile
{
    public ApiProfile()
    {
        // Product mappings
        CreateMap<FinishedProductDomain, FinishedProductRequest>()
            .ForMember(dest => dest.Orders, opt => opt.MapFrom(src => src.Orders));
        
        // ProductionOrder mappings
        CreateMap<ProductionOrderDomain, ProductionOrderRequest>()
            .ForMember(dest => dest.Components, opt => opt.MapFrom(src => src.Components));
        
        // ProductComponent mappings
        CreateMap<ProductComponentDomain, ProductComponentRequest>()
            .ForMember(dest => dest.ProductType, opt => opt.MapFrom(src => src.ProductType))
            .ForMember(dest => dest.Warehouse, opt => opt.MapFrom(src => src.Warehouse));
        
        // ProductType mappings
        CreateMap<ProductTypeDomain, ProductTypeRequest>();
        
        // Warehouse mappings
        CreateMap<WarehouseDomain, WarehouseRequest>();
        
        // Supplier mappings
        CreateMap<SupplierDomain, SupplierRequest>();
        
        // Supply mappings
        CreateMap<SupplyDomain, SupplyRequest>();
        
        CreateMap<UserDomain, UserRequest>();
    }
}