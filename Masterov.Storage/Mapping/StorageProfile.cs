using AutoMapper;
using Masterov.Domain.Models;

namespace Masterov.Storage.Mapping;

internal class StorageProfile : Profile
{
    public StorageProfile()
    {
        // FinishedProduct → FinishedProductDomain
        CreateMap<FinishedProduct, FinishedProductDomain>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.ProductType.Name))
            .ForMember(dest => dest.Orders, opt => opt.MapFrom(src => src.Orders));

        // FinishedProduct → FinishedProductNoOrdersDomain
        CreateMap<FinishedProduct, FinishedProductNoOrdersDomain>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.ProductType.Name));

        // ProductType → ProductTypeDomain
        CreateMap<ProductType, ProductTypeDomain>();

        // Order
        CreateMap<Order, OrderDomain>()
            .ForMember(dest => dest.UsedComponents, opt => opt.MapFrom(src => src.UsedComponents))
            .ForMember(dest => dest.Payments, opt => opt.MapFrom(src => src.Payments))
            .ForMember(dest => dest.FinishedProductId, opt => opt.MapFrom(src => src.FinishedProduct.FinishedProductId))
            .ForMember(dest => dest.FullPriceFinishedProduct, opt => opt.MapFrom(src => src.FinishedProduct.Price));

        // UsedComponent
        CreateMap<UsedComponent, UsedComponentDomain>()
            .ForMember(dest => dest.ComponentType, opt => opt.MapFrom(src => src.ComponentType))
            .ForMember(dest => dest.Warehouse, opt => opt.MapFrom(src => src.Warehouse))
            .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderId));

        // Customer
        CreateMap<Customer, CustomerDomain>()
            .ForMember(dest => dest.Orders, opt => opt.MapFrom(src => src.Orders));

        // Payment
        CreateMap<Payment, PaymentDomain>()
            .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer));

        // User
        CreateMap<User, UserDomain>()
            .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));

        // Warehouse + ComponentType
        CreateMap<ComponentType, ComponentTypeDomain>();
        CreateMap<Warehouse, WarehouseDomain>();

        // Supplier
        CreateMap<Supplier, SupplierDomain>();

        // Supply
        CreateMap<Supply, SupplyDomain>();
    }
}