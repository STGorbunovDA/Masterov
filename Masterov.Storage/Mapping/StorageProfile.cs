using AutoMapper;
using Masterov.Domain.Models;

namespace Masterov.Storage.Mapping;

internal class StorageProfile : Profile
{
    public StorageProfile()
    {
        CreateMap<Product, ProductDomain>()
            .ForMember(dest => dest.Type, 
                opt => opt.MapFrom(src => src.ProductType != null ? src.ProductType.Name : null));
    }
}