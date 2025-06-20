﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.Customer.GetCustomerByName;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Masterov.Storage.Storages.Masterov.Customer;

internal class GetCustomerByNameStorage(MasterovDbContext dbContext, IMemoryCache memoryCache, IMapper mapper) : IGetCustomerByNameStorage
{
    public async Task<CustomerDomain?> GetCustomerByName(string customerName, CancellationToken cancellationToken)=>
        (await memoryCache.GetOrCreateAsync<CustomerDomain?>( 
            nameof(GetCustomerByName),
            entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(1);
                return dbContext.Customers
                    .Where(f => f.Name.ToLower() == customerName.ToLower().Trim())
                    .ProjectTo<CustomerDomain>(mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(cancellationToken);
            }))!;
}