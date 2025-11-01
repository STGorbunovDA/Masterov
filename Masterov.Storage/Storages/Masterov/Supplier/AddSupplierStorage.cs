using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.Supplier.AddSupplier;
using Masterov.Domain.Models;
using Masterov.Storage.Extension;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Supplier;

internal class AddSupplierStorage(MasterovDbContext dbContext, IGuidFactory guidFactory, IMapper mapper) : IAddSupplierStorage
{
    public async Task<SupplierDomain> AddSupplier(string name, string surname, string? email, string? phone, string? address,
        CancellationToken cancellationToken)
    {
        var supplierId = guidFactory.Create();

        var supplier = new Storage.Supplier
        {
            SupplierId = supplierId,
            Name = name,
            Surname = surname,
            Email = email,
            Phone = phone,
            Address = address,
            CreatedAt = DateTime.Now
        };

        await dbContext.Suppliers.AddAsync(supplier, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return await dbContext.Suppliers
            .Where(t => t.SupplierId == supplierId)
            .ProjectTo<SupplierDomain>(mapper.ConfigurationProvider)
            .FirstAsync(cancellationToken);
    }
    
}