using AutoMapper;
using Masterov.Domain.Models;

namespace Masterov.Storage.Mapping;

internal class StorageProfile : Profile
{
    public StorageProfile()
    {
        CreateMap<Product, ProductDomain>();
    }
}