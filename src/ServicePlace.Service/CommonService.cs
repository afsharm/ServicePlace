using ServicePlace.Data;
using Microsoft.EntityFrameworkCore;
using ServicePlace.Model.Queries;
using ServicePlace.Model.Commands;
using ServicePlace.Model;
using Microsoft.Extensions.Logging;

namespace ServicePlace.Service;

public class CommonService
{
    private readonly ServicePlaceContext _context;
    private readonly ILogger<ServicePlaceContext> _logger;

    public CommonService(ServicePlaceContext context, ILogger<ServicePlaceContext> logger)
    {
        _context = context;
        _logger = logger;
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
        ValidateCreateService(command);

        await _context.Services.AddAsync(new Model.Entities.Service
        {
            Name = command.Name
        });
    }

    public void ValidateCreateService(CreateService command)
    {
        if (command.Name?.Length > 100)
            throw new Exception();
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

    public async Task UpdateProviderAsync(int id, string name)
    {
        _logger.LogDebug($"UpdateProviderAsync => {id}, {name}");
        var provider = await _context.Providers.Where(x => x.Id == id).FirstOrDefaultAsync();

        if (provider == null)
            throw new NotFoundException();

        provider.Name = name;
        _context.Providers.Update(provider);
    }
}
