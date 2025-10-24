using AutoMapper;
using Masterov.API.Models.ComponentType;
using Masterov.API.Models.Customer;
using Masterov.API.Models.FinishedProduct;
using Masterov.API.Models.Order;
using Masterov.API.Models.Payment;
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
        CreateMap<ComponentTypeDomain, ComponentTypeResponse>();
        CreateMap<WarehouseDomain, WarehouseNewResponse>();
        CreateMap<WarehouseDomain, WarehouseForOrderRequest>();
        CreateMap<WarehouseDomain, WarehouseNewNoProductTypeRequest>();
        CreateMap<WarehouseDomain, WarehouseResponse>();

        // 2. Затем маппинг ProductComponent с указанием зависимостей
        CreateMap<UsedComponentDomain, UsedComponentNewRequest>()
            .ForMember(dest => dest.ComponentType, opt => opt.MapFrom(src => src.ComponentType)) // AutoMapper сам применит маппинг ProductType
            .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderId)) // AutoMapper сам применит маппинг OrderId
            .ForMember(dest => dest.Warehouse, opt => opt.MapFrom(src => src.Warehouse));   // AutoMapper сам применит маппинг Warehouse

        // 2. Затем маппинг ProductComponent с указанием зависимостей
        CreateMap<UsedComponentDomain, UsedComponentResponse>()
            .ForMember(dest => dest.ComponentType, opt => opt.MapFrom(src => src.ComponentType)) // AutoMapper сам применит маппинг ProductType
            .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderId)) // AutoMapper сам применит маппинг OrderId
            .ForMember(dest => dest.Warehouse, opt => opt.MapFrom(src => src.Warehouse));   // AutoMapper сам применит маппинг Warehouse

        // 3. Маппинг ProductionOrder с преобразованием коллекций
        CreateMap<OrderDomain, OrderResponse>()
            .ForMember(dest => dest.UsedComponents, opt => opt.MapFrom(src => src.UsedComponents))
            .ForMember(dest => dest.Payments, opt => opt.MapFrom(src => src.Payments))
            .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer))
            .ForMember(dest => dest.FinishedProductId, opt => opt.MapFrom(src => src.FinishedProductId))
            .ForMember(dest => dest.FullPriceFinishedProduct, opt => opt.MapFrom(src => src.FullPriceFinishedProduct))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));; // Автоматически применит маппинг ProductComponent

        CreateMap<OrderDomain, OrderNoPaymentsResponse>()
            .ForMember(dest => dest.UsedComponents, opt => opt.MapFrom(src => src.UsedComponents))
            .ForMember(dest => dest.CustomerNoOrders, opt => opt.MapFrom(src => src.Customer))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString())); // Автоматически применит маппинг ProductComponent
        
        CreateMap<OrderDomain, OrderNoCustumerResponse>()
            .ForMember(dest => dest.UsedComponents, opt => opt.MapFrom(src => src.UsedComponents))
            .ForMember(dest => dest.PaymentsNoCustomer, opt => opt.MapFrom(src => src.Payments))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));; // Автоматически применит маппинг ProductComponent
        
        CreateMap<OrderDomain, OrderNoUsedComponentResponse>()
            .ForMember(dest => dest.FinishedProductId, opt => opt.MapFrom(src => src.FinishedProductId))
            .ForMember(dest => dest.FullPriceFinishedProduct, opt => opt.MapFrom(src => src.FullPriceFinishedProduct))
            .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));; // Автоматически применит маппинг ProductComponent
        
        CreateMap<OrderDomain, OrderNoCustomerNoUsedComponentsRequest>()
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
            .ForMember(dest => dest.ComponentType, opt => opt.MapFrom(src => src.ComponentType));
        CreateMap<SupplierDomain, SupplierRequestNoSupply>();
        
        CreateMap<SupplyDomain, SupplyNewResponse>()
            .ForMember(dest => dest.Supplier, opt => opt.MapFrom(src => src.Supplier))
            .ForMember(dest => dest.ComponentType, opt => opt.MapFrom(src => src.ComponentType))
            .ForMember(dest => dest.WarehouseNew, opt => opt.MapFrom(src => src.Warehouse));

        CreateMap<SupplyDomain, SupplyNoWarehouseNewResponse>()
            .ForMember(dest => dest.Supplier, opt => opt.MapFrom(src => src.Supplier))
            .ForMember(dest => dest.ComponentType, opt => opt.MapFrom(src => src.ComponentType));
        
        CreateMap<CustomerDomain, CustomerNoOrdersResponse>();
        CreateMap<FinishedProductNoOrdersDomain, FinishedProductNoOrdersResponse>();
    }
}