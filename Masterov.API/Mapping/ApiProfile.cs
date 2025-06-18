using AutoMapper;
using Masterov.API.Models;
using Masterov.API.Models.Auth;
using Masterov.API.Models.Customer;
using Masterov.API.Models.FinishedProduct;
using Masterov.API.Models.ProductionOrder;
using Masterov.API.Models.ProductType;
using Masterov.Domain.Models;

internal class ApiProfile : Profile
{
    public ApiProfile()
    {
        // 1. Сначала маппинг простых зависимостей (ProductType и Warehouse)
        CreateMap<ProductTypeDomain, ProductTypeRequest>();
        CreateMap<WarehouseDomain, WarehouseRequest>();

        // 2. Затем маппинг ProductComponent с указанием зависимостей
        CreateMap<ProductComponentDomain, ProductComponentRequest>()
            .ForMember(dest => dest.ProductType, opt => opt.MapFrom(src => src.ProductType)) // AutoMapper сам применит маппинг ProductType
            .ForMember(dest => dest.Warehouse, opt => opt.MapFrom(src => src.Warehouse));   // AutoMapper сам применит маппинг Warehouse

        // 3. Маппинг ProductionOrder с преобразованием коллекций
        CreateMap<ProductionOrderDomain, ProductionOrderRequest>()
            .ForMember(dest => dest.Components, opt => opt.MapFrom(src => src.Components)); // Автоматически применит маппинг ProductComponent

        // 4. Маппинг FinishedProduct (если нужен)
        CreateMap<FinishedProductDomain, FinishedProductRequest>()
            .ForMember(dest => dest.Orders, opt => opt.MapFrom(src => src.Orders)); // Автоматически применит маппинг ProductionOrder

        CreateMap<CustomerDomain, CustomerRequest>()
            .ForMember(dest => dest.Orders, opt => opt.MapFrom(src => src.Orders)); // Автоматически применит маппинг ProductionOrder

        
        // Остальные маппинги
        CreateMap<SupplierDomain, SupplierRequest>();
        CreateMap<SupplyDomain, SupplyRequest>();
        CreateMap<UserDomain, UserRequest>();
    }
}