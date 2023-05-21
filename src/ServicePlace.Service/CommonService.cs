using ServicePlace.Data;
using Microsoft.EntityFrameworkCore;
using ServicePlace.Model.Queries;
using ServicePlace.Model.Commands;
using ServicePlace.Model;
using Microsoft.Extensions.Logging;
using ServicePlace.Model.Results;

namespace ServicePlace.Service;

public class CommonService
{
    private readonly ServicePlaceContext _context;
    private readonly ILogger<CommonService> _logger;

    public CommonService(ServicePlaceContext context, ILogger<CommonService> logger)
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

    public async Task<CreateServiceResult> CreateServiceAsync(CreateService command)
    {
        ValidateCreateService(command);
        var service = new Model.Entities.Service
        {
            Name = command.Name
        };
        await _context.Services.AddAsync(service);

        //doing save changes is necessary here because we need to return database generated id without exposing Entities to upper layers
        await _context.SaveChangesAsync();

        return new CreateServiceResult
        {
            ServiceId = service.Id
        };
    }

    void ValidateCreateService(CreateService command)
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

    public async Task<CreateProviderResult> CreateProviderAsync(CreateProviderCommand? command)
    {
        if (command == null)
            throw new ArgumentNullException("command");

        if (command.ServiceId == null)
            throw new ArgumentNullException("command.ServiceId");

        var newProvider = new Model.Entities.Provider { Name = command.Name, ServiceId = command.ServiceId.Value };

        await _context.Providers.AddAsync(newProvider);
        await _context.SaveChangesAsync();

        return new CreateProviderResult { ProviderId = newProvider.Id };
    }
}
