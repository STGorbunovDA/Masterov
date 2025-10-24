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
        CreateMap<Order, OrderDomain>()
            .ForMember(dest => dest.UsedComponents, opt => opt.MapFrom(src => src.UsedComponents))
            .ForMember(dest => dest.Payments, opt => opt.MapFrom(src => src.Payments))
            .ForMember(dest => dest.FinishedProductId, opt => opt.MapFrom(src => src.FinishedProduct.FinishedProductId))
            .ForMember(dest => dest.FullPriceFinishedProduct, opt => opt.MapFrom(src => src.FinishedProduct.Price));
            
        // ProductComponent mapping
        CreateMap<UsedComponent, UsedComponentDomain>()
            .ForMember(dest => dest.ComponentType, opt => opt.MapFrom(src => src.ComponentType))
            .ForMember(dest => dest.Warehouse, opt => opt.MapFrom(src => src.Warehouse))
            .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderId));
            
        // Product mapping
        CreateMap<Customer, CustomerDomain>()
            .ForMember(dest => dest.Orders, opt => opt.MapFrom(src => src.Orders));
        
        CreateMap<Payment, PaymentDomain>()
            .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer));
        
        CreateMap<User, UserDomain>()
            .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer));

        
        // ProductType mapping
        CreateMap<ComponentType, ComponentTypeDomain>();
            
        // Warehouse mapping
        CreateMap<Warehouse, WarehouseDomain>();
        // Supplier mapping (если нужно)
        CreateMap<Supplier, SupplierDomain>();
            
        // Supply mapping (если нужно)
        CreateMap<Supply, SupplyDomain>();
        
        CreateMap<FinishedProduct, FinishedProductNoOrdersDomain>();
    }
}