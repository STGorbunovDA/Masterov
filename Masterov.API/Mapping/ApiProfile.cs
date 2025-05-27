using AutoMapper;
using Masterov.API.Models;
using Masterov.Domain.Models;

namespace Masterov.API.Mapping;

internal class ApiProfile : Profile
{
    public ApiProfile()
    {
        CreateMap<ProductDomain, ProductRequest>();
        CreateMap<ProductTypeDomain, ProductTypeRequest>();
    }
}