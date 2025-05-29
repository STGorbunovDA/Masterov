using AutoMapper;
using Masterov.Domain.Models;

namespace Masterov.Storage.Mapping;

internal class StorageProfile : Profile
{
    public StorageProfile()
    {
        CreateMap<ProductType, ProductTypeDomain>();
        CreateMap<Product, ProductDomain>()
            .ForMember(dest => dest.ProductComponents,
                opt => opt.MapFrom(src => 
                    src.ProductComponents.Select(pc => $"{pc.ProductType.Name} x {pc.Quantity}").ToArray()));

    }
}