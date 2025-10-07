using AutoMapper;
using Masterov.API.Models;
using Masterov.API.Models.Auth;
using Masterov.API.Models.Customer;
using Masterov.API.Models.FinishedProduct;
using Masterov.API.Models.Order;
using Masterov.API.Models.Payment;
using Masterov.API.Models.ProductComponent;
using Masterov.API.Models.ProductType;
using Masterov.API.Models.Supplier;
using Masterov.API.Models.Supply;
using Masterov.API.Models.User;
using Masterov.API.Models.Warehouse;
using Masterov.Domain.Models;
using SupplierRequest = Masterov.API.Models.Supplier.SupplierRequest;

internal class ApiProfile : Profile
{
    public ApiProfile()
    {
        // 1. Сначала маппинг простых зависимостей (ProductType и Warehouse)
        CreateMap<ProductTypeDomain, ProductTypeRequest>();
        CreateMap<WarehouseDomain, WarehouseNewRequest>();
        CreateMap<WarehouseDomain, WarehouseRequest>();

        // 2. Затем маппинг ProductComponent с указанием зависимостей
        CreateMap<ProductComponentDomain, ProductComponentRequest>()
            .ForMember(dest => dest.ProductType, opt => opt.MapFrom(src => src.ProductType)) // AutoMapper сам применит маппинг ProductType
            .ForMember(dest => dest.WarehouseNew, opt => opt.MapFrom(src => src.Warehouse));   // AutoMapper сам применит маппинг Warehouse

        // 3. Маппинг ProductionOrder с преобразованием коллекций
        CreateMap<OrderDomain, OrderRequest>()
            .ForMember(dest => dest.Components, opt => opt.MapFrom(src => src.Components))
            .ForMember(dest => dest.PaymentsNoCustomer, opt => opt.MapFrom(src => src.Payments))
            .ForMember(dest => dest.CustomerNoOrders, opt => opt.MapFrom(src => src.Customer))
            .ForMember(dest => dest.FullPriceFinishedProduct, opt => opt.MapFrom(src => src.FullPriceFinishedProduct))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));; // Автоматически применит маппинг ProductComponent

        CreateMap<OrderDomain, OrderRequestNoPayments>()
            .ForMember(dest => dest.Components, opt => opt.MapFrom(src => src.Components))
            .ForMember(dest => dest.CustomerNoOrders, opt => opt.MapFrom(src => src.Customer))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));; // Автоматически применит маппинг ProductComponent
        
        CreateMap<OrderDomain, OrderRequestNoCustumer>()
            .ForMember(dest => dest.Components, opt => opt.MapFrom(src => src.Components))
            .ForMember(dest => dest.PaymentsNoCustomer, opt => opt.MapFrom(src => src.Payments))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));; // Автоматически применит маппинг ProductComponent

        
        CreateMap<OrderDomain, OrderNoCustomerNoComponentsRequest>()
            .ForMember(dest => dest.PaymentsNoCustomer, opt => opt.MapFrom(src => src.Payments))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));; // Автоматически применит маппинг ProductComponent
        
        CreateMap<OrderDomain, OrderNoCustomerNoComponentsNoPaymentsRequest>()
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
        CreateMap<SupplierDomain, SupplierNewRequest>();
        CreateMap<SupplyDomain, SupplyRequest>()
            .ForMember(dest => dest.WarehouseNew, opt => opt.MapFrom(src => src.Warehouse))
            .ForMember(dest => dest.Supplier, opt => opt.MapFrom(src => src.Supplier))
            .ForMember(dest => dest.ProductType, opt => opt.MapFrom(src => src.ProductType));
        CreateMap<SupplierDomain, SupplierRequestNoSupply>();
        
        CreateMap<SupplyDomain, SupplyNewRequest>()
            .ForMember(dest => dest.Supplier, opt => opt.MapFrom(src => src.Supplier))
            .ForMember(dest => dest.ProductType, opt => opt.MapFrom(src => src.ProductType))
            .ForMember(dest => dest.WarehouseNew, opt => opt.MapFrom(src => src.Warehouse));

        CreateMap<SupplyDomain, SupplyNewRequestNoWarehouse>()
            .ForMember(dest => dest.Supplier, opt => opt.MapFrom(src => src.Supplier))
            .ForMember(dest => dest.ProductType, opt => opt.MapFrom(src => src.ProductType));
        
        CreateMap<CustomerDomain, CustomerNoOrdersRequest>();
    }
}