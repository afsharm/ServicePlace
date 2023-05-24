using Microsoft.EntityFrameworkCore;
using ServicePlace.Model.Queries;
using ServicePlace.Model.Contracts;
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
            .Select(x => new ProviderDisplay
            {
                Id = x.Id,
                Name = x.Name
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<ProviderDisplay>> GetProviderByServiceIdAsync(int serviceId)
    {
        return await _context.Providers
            .Where(x => x.Service.Id == serviceId)
            .Select(x => new ProviderDisplay
            {
                Id = x.Id,
                Name = x.Name
            })
            .ToListAsync();
    }

    public async Task<Provider?> GetProviderAsync(int id)
    {
        var provider = await _context.Providers.Where(x => x.Id == id).FirstOrDefaultAsync();

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
}