using AutoMapper;
using Masterov.API.Models;
using Masterov.API.Models.Auth;
using Masterov.API.Models.Customer;
using Masterov.API.Models.FinishedProduct;
using Masterov.API.Models.Payment;
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
            .ForMember(dest => dest.Components, opt => opt.MapFrom(src => src.Components))
            .ForMember(dest => dest.PaymentsNoCustomer, opt => opt.MapFrom(src => src.Payments))
            .ForMember(dest => dest.CustomerNoOrders, opt => opt.MapFrom(src => src.Customer))
            .ForMember(dest => dest.FullPriceFinishedProduct, opt => opt.MapFrom(src => src.FullPriceFinishedProduct))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));; // Автоматически применит маппинг ProductComponent

        CreateMap<ProductionOrderDomain, ProductionOrderRequestNoPayments>()
            .ForMember(dest => dest.Components, opt => opt.MapFrom(src => src.Components))
            .ForMember(dest => dest.CustomerNoOrders, opt => opt.MapFrom(src => src.Customer))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));; // Автоматически применит маппинг ProductComponent
        
        CreateMap<ProductionOrderDomain, ProductionOrderRequestNoCustumer>()
            .ForMember(dest => dest.Components, opt => opt.MapFrom(src => src.Components))
            .ForMember(dest => dest.PaymentsNoCustomer, opt => opt.MapFrom(src => src.Payments))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));; // Автоматически применит маппинг ProductComponent

        
        CreateMap<ProductionOrderDomain, ProductionOrderNoCustomerNoComponentsRequest>()
            .ForMember(dest => dest.PaymentsNoCustomer, opt => opt.MapFrom(src => src.Payments))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));; // Автоматически применит маппинг ProductComponent
        
        CreateMap<ProductionOrderDomain, ProductionOrderNoCustomerNoComponentsNoPaymentsRequest>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));; // Автоматически применит маппинг ProductComponent
        
        // 4. Маппинг FinishedProduct (если нужен)
        CreateMap<FinishedProductDomain, FinishedProductRequest>()
            .ForMember(dest => dest.Orders, opt => opt.MapFrom(src => src.Orders)); // Автоматически применит маппинг ProductionOrder

        CreateMap<CustomerDomain, CustomerRequest>()
            .ForMember(dest => dest.Orders, opt => opt.MapFrom(src => src.Orders)); // Автоматически применит маппинг ProductionOrder
        CreateMap<PaymentDomain, PaymentRequest>()
            .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer));
        
        CreateMap<CustomerDomain, CustomerNewRequest>()
            .ForMember(dest => dest.Orders, opt => opt.MapFrom(src => src.Orders)); // Автоматически применит маппинг ProductionOrder
        
        CreateMap<PaymentDomain, PaymentsNewCustomerRequest>()
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.Name))
            .ForMember(dest => dest.CustomerEmail, opt => opt.MapFrom(src => src.Customer.Email))
            .ForMember(dest => dest.CustomerPhone, opt => opt.MapFrom(src => src.Customer.Phone));

        CreateMap<UserDomain, UserRequest>()
            .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer));

        
        // Остальные маппинги
        CreateMap<SupplierDomain, SupplierRequest>();
        CreateMap<SupplyDomain, SupplyRequest>();
        CreateMap<CustomerDomain, CustomerNoOrdersRequest>();
    }
}