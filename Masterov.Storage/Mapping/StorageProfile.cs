using AutoMapper;
using Masterov.Domain.Models;

namespace Masterov.Storage.Mapping;

internal class StorageProfile : Profile
{
    public StorageProfile()
    {
        // Product mapping
        CreateMap<FinishedProduct, FinishedProductDomain>()
            .ForMember(dest => dest.Orders, opt => opt.MapFrom(src => src.Orders));
            
        // ProductionOrder mapping
        CreateMap<ProductionOrder, ProductionOrderDomain>()
            .ForMember(dest => dest.Components, opt => opt.MapFrom(src => src.Components))
            .ForMember(dest => dest.Payments, opt => opt.MapFrom(src => src.Payments));
            
        // ProductComponent mapping
        CreateMap<ProductComponent, ProductComponentDomain>()
            .ForMember(dest => dest.ProductType, opt => opt.MapFrom(src => src.ProductType))
            .ForMember(dest => dest.Warehouse, opt => opt.MapFrom(src => src.Warehouse));
            
        // Product mapping
        CreateMap<Customer, CustomerDomain>()
            .ForMember(dest => dest.Orders, opt => opt.MapFrom(src => src.Orders));
        
        // ProductType mapping
        CreateMap<ProductType, ProductTypeDomain>();
            
        // Warehouse mapping
        CreateMap<Warehouse, WarehouseDomain>();
            
        // Supplier mapping (если нужно)
        CreateMap<Supplier, SupplierDomain>();
            
        // Supply mapping (если нужно)
        CreateMap<Supply, SupplyDomain>();
        
        CreateMap<User, UserDomain>();
        CreateMap<OrderPayment, PaymentDomain>();
    }
}