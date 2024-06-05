using Microsoft.EntityFrameworkCore;
using ServicePlace.Model.Queries;
using ServicePlace.Data.Contracts;
using ServicePlace.Model.Entities;

namespace ServicePlace.Data.Repositories;

public class ProviderRepository : IProviderRepository
{
    ServicePlaceContext _context;

    public ProviderRepository(ServicePlaceContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProviderDisplay>> GetAllProvidersAsync()
    {
        return await _context.Providers
            .Where(x => x.IsDeleted == false)
            .Select(x => new ProviderDisplay
            {
                Id = x.Id,
                Name = x.Name,
                ServiceName = x.Service.Name
            })
            .ToListAsync();
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

    public async Task<Provider?> GetProviderAsync(int id)
    {
        var provider = await _context.Providers.Where(x => x.Id == id && x.IsDeleted == false).FirstOrDefaultAsync();

        return provider;
    }

    public void UpdateProvider(Provider provider)
    {
        _context.Providers.Update(provider);
    }

    public async Task<bool> AnyDuplicateAsync(string? name, int? serviceId)
    {
        var anyDuplicate = await _context.Providers.AnyAsync(x => x.Name == name && x.ServiceId == serviceId);
        return anyDuplicate;
    }

    public async Task AddProviderAsync(Provider newProvider)
    {
        await _context.Providers.AddAsync(newProvider);
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