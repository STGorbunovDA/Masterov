using AutoMapper;
using Masterov.API.Models.Customer;
using Masterov.API.Models.FinishedProduct;
using Masterov.API.Models.Order;
using Masterov.API.Models.Payment;
using Masterov.API.Models.ProductType;
using Masterov.API.Models.Supplier;
using Masterov.API.Models.Supply;
using Masterov.API.Models.UsedComponent;
using Masterov.API.Models.User;
using Masterov.API.Models.Warehouse;
using Masterov.Domain.Models;

internal class ApiProfile : Profile
{
    public ApiProfile()
    {
        // 1. Сначала маппинг простых зависимостей (ProductType и Warehouse)
        CreateMap<ProductTypeDomain, ProductTypeResponse>();
        CreateMap<WarehouseDomain, WarehouseNewResponse>();
        CreateMap<WarehouseDomain, WarehouseForOrderRequest>();
        CreateMap<WarehouseDomain, WarehouseNewNoProductTypeRequest>();
        CreateMap<WarehouseDomain, WarehouseResponse>();

        // 2. Затем маппинг ProductComponent с указанием зависимостей
        CreateMap<UsedComponentDomain, UsedComponentNewRequest>()
            .ForMember(dest => dest.ProductType, opt => opt.MapFrom(src => src.ProductType)) // AutoMapper сам применит маппинг ProductType
            .ForMember(dest => dest.Warehouse, opt => opt.MapFrom(src => src.Warehouse));   // AutoMapper сам применит маппинг Warehouse

        // 2. Затем маппинг ProductComponent с указанием зависимостей
        CreateMap<UsedComponentDomain, UsedComponentResponse>()
            .ForMember(dest => dest.ProductType, opt => opt.MapFrom(src => src.ProductType)) // AutoMapper сам применит маппинг ProductType
            .ForMember(dest => dest.Warehouse, opt => opt.MapFrom(src => src.Warehouse));   // AutoMapper сам применит маппинг Warehouse

        // 3. Маппинг ProductionOrder с преобразованием коллекций
        CreateMap<OrderDomain, OrderResponse>()
            .ForMember(dest => dest.Components, opt => opt.MapFrom(src => src.Components))
            .ForMember(dest => dest.Payments, opt => opt.MapFrom(src => src.Payments))
            .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer))
            .ForMember(dest => dest.FinishedProductId, opt => opt.MapFrom(src => src.FinishedProductId))
            .ForMember(dest => dest.FullPriceFinishedProduct, opt => opt.MapFrom(src => src.FullPriceFinishedProduct))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));; // Автоматически применит маппинг ProductComponent

        CreateMap<OrderDomain, OrderNoPaymentsResponse>()
            .ForMember(dest => dest.Components, opt => opt.MapFrom(src => src.Components))
            .ForMember(dest => dest.CustomerNoOrders, opt => opt.MapFrom(src => src.Customer))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));; // Автоматически применит маппинг ProductComponent
        
        CreateMap<OrderDomain, OrderNoCustumerResponse>()
            .ForMember(dest => dest.Components, opt => opt.MapFrom(src => src.Components))
            .ForMember(dest => dest.PaymentsNoCustomer, opt => opt.MapFrom(src => src.Payments))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));; // Автоматически применит маппинг ProductComponent

        
        CreateMap<OrderDomain, OrderNoCustomerNoCUsedComponentsRequest>()
            .ForMember(dest => dest.PaymentsNoCustomer, opt => opt.MapFrom(src => src.Payments))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));; // Автоматически применит маппинг ProductComponent
        
        CreateMap<OrderDomain, OrderNoCustomerNoUsedComponentsNoPaymentsRequest>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));; // Автоматически применит маппинг ProductComponent
        
        // 4. Маппинг FinishedProduct (если нужен)
        CreateMap<FinishedProductDomain, FinishedProductResponse>()
            .ForMember(dest => dest.Orders, opt => opt.MapFrom(src => src.Orders)); // Автоматически применит маппинг ProductionOrder

        CreateMap<CustomerDomain, CustomerResponse>()
            .ForMember(dest => dest.Orders, opt => opt.MapFrom(src => src.Orders)); // Автоматически применит маппинг ProductionOrder
        CreateMap<PaymentDomain, PaymentResponse>()
            .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer));
        
        CreateMap<CustomerDomain, CustomerNewResponse>()
            .ForMember(dest => dest.Orders, opt => opt.MapFrom(src => src.Orders)); // Автоматически применит маппинг ProductionOrder

        CreateMap<PaymentDomain, PaymentsNewCustomerRequest>();
        CreateMap<PaymentDomain, PaymentNewResponse>();
        CreateMap<UserDomain, UserResponse>()
            .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer));

        
        // Остальные маппинги
        CreateMap<SupplierDomain, SupplierResponse>();
        CreateMap<SupplierDomain, SupplierNewResponse>();
        CreateMap<SupplyDomain, SupplyRequest>()
            .ForMember(dest => dest.WarehouseNew, opt => opt.MapFrom(src => src.Warehouse))
            .ForMember(dest => dest.Supplier, opt => opt.MapFrom(src => src.Supplier))
            .ForMember(dest => dest.ProductType, opt => opt.MapFrom(src => src.ProductType));
        CreateMap<SupplierDomain, SupplierRequestNoSupply>();
        
        CreateMap<SupplyDomain, SupplyNewResponse>()
            .ForMember(dest => dest.Supplier, opt => opt.MapFrom(src => src.Supplier))
            .ForMember(dest => dest.ProductType, opt => opt.MapFrom(src => src.ProductType))
            .ForMember(dest => dest.WarehouseNew, opt => opt.MapFrom(src => src.Warehouse));

        CreateMap<SupplyDomain, SupplyNoWarehouseNewResponse>()
            .ForMember(dest => dest.Supplier, opt => opt.MapFrom(src => src.Supplier))
            .ForMember(dest => dest.ProductType, opt => opt.MapFrom(src => src.ProductType));
        
        CreateMap<CustomerDomain, CustomerNoOrdersResponse>();
        CreateMap<FinishedProductNoOrdersDomain, FinishedProductNoOrdersResponse>();
    }
}