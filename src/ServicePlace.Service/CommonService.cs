using ServicePlace.Data;
using ServicePlace.Model.Entities;
using Microsoft.EntityFrameworkCore;
using ServicePlace.Model.Queries;
using ServicePlace.Model.Commands;

namespace ServicePlace.Service;

public class CommonService
{
    private readonly ServicePlaceContext _context;

    public CommonService(ServicePlaceContext context)
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

    public async Task<IEnumerable<ServiceDisplay>> GetServicesAsync()
    {
        return await _context.Services
            .Select(x => new ServiceDisplay
            {
                Id = x.Id,
                Name = x.Name,
            })
            .ToListAsync();
    }

    public async Task CreateServiceAsync(CreateService command)
    {
        await _context.Services.AddAsync(new Model.Entities.Service
        {
            Name = command.Name
        });
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
}
