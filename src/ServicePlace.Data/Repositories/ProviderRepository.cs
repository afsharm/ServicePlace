using Microsoft.EntityFrameworkCore;
using ServicePlace.Core.Queries;
using ServicePlace.Core.Results;
using ServicePlace.Core.Contracts;
using ServicePlace.Core.DomainEntities;
using ServicePlace.Data.DatabaseEntities;
using System.Linq.Expressions;

namespace ServicePlace.Data.Repositories;

public class ProviderRepository : IProviderRepository
{
    ServicePlaceContext _context;

    public ProviderRepository(ServicePlaceContext context)
    {
        _context = context;
    }

    public async Task<PagingResult<ProviderDisplay>> GetAllProvidersAsync(ProviderPagingQuery query)
    {
        var providersQuery = _context.Providers.Where(x => x.IsDeleted == false);

        if (string.IsNullOrWhiteSpace(query.Criteria) == false)
            providersQuery = providersQuery.Where(x => x.Name.Contains(query.Criteria) || x.Service.Name.Contains(query.Criteria));

        var result = new PagingResult<ProviderDisplay>
        {

            Items = await providersQuery
                .Select(x => new ProviderDisplay
                {
                    Id = x.Id,
                    Name = x.Name,
                    ServiceName = x.Service.Name
                })
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync(),
            TotalCount = await providersQuery.CountAsync()
        };

        return result;
    }

    public async Task<IEnumerable<ProviderDisplay>> GetProviderByServiceIdAsync(int serviceId)
    {
        return await _context.Providers
            .Where(x => x.Service.Id == serviceId && x.IsDeleted == false)
            .Select(x => new ProviderDisplay
            {
                Id = x.Id,
                Name = x.Name
            })
            .ToListAsync();
    }

    public async Task<ProviderDomain?> GetProviderAsync(int id)
    {
        var provider = await _context.Providers.Where(x => x.Id == id && x.IsDeleted == false).FirstOrDefaultAsync();

        if (provider == null)
            throw new Exception($"No `Provider` found with the given id `{id}`");

        var providerDomain = new ProviderDomain
        {
            Id = provider.Id,
            Name = provider.Name,
            ServiceId = provider.ServiceId
        };

        return providerDomain;
    }

    public void UpdateProvider(ProviderDomain providerDomain)
    {
        var provider = new Provider
        {
            Id = providerDomain.Id,
            Name = providerDomain.Name,
            ServiceId = providerDomain.ServiceId
        };

        _context.Providers.Update(provider);
    }

    public async Task<bool> AnyDuplicateAsync(string? name, int? serviceId)
    {
        var anyDuplicate = await _context.Providers.AnyAsync(x => x.Name == name && x.ServiceId == serviceId);
        return anyDuplicate;
    }

    public async Task AddProviderAsync(ProviderDomain newProviderDomain)
    {
        var provider = new Provider
        {
            Name = newProviderDomain.Name,
            ServiceId = newProviderDomain.ServiceId
        };

        await _context.Providers.AddAsync(provider);
    }

    public async Task<ProviderDisplay?> GetProviderByIdAsync(int providerId)
    {
        var provider = await _context.Providers
            .Where(x => x.Id == providerId && x.IsDeleted == false)
            .Select(x => new ProviderDisplay
            {
                Id = x.Id,
                Name = x.Name,
                ServiceId = x.ServiceId,
                ServiceName = x.Service.Name
            })
            .FirstOrDefaultAsync();

        return provider;
    }

    public async Task DeleteAsync(int providerId)
    {
        var provider = await _context.Providers.Where(x => x.Id == providerId).FirstOrDefaultAsync();

        if (provider == null)
            throw new Exception($"Provider not found with the given Id {providerId}");

        provider.IsDeleted = true;

        _context.Providers.Update(provider);
    }
}