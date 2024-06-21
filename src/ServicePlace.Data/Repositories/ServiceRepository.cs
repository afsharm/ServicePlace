using Microsoft.EntityFrameworkCore;
using ServicePlace.Core.Queries;
using ServicePlace.Data.DatabaseEntities;
using ServicePlace.Core.Commands;
using ServicePlace.Core.Contracts;
using ServicePlace.Core.DomainEntities;

namespace ServicePlace.Data.Repositories;

public class ServiceRepository : IServiceRepository
{
    ServicePlaceContext _context;

    public ServiceRepository(ServicePlaceContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ServiceDisplay>> GetServicesAsync()
    {
        return await _context.Services
            .Where(x => x.IsDeleted == false)
            .Select(x => new ServiceDisplay
            {
                Id = x.Id,
                Name = x.Name,
            })
            .ToListAsync();
    }

    public async Task AddAsync(ServiceDomain serviceDomain)
    {
        var service = new Service
        {
            Name = serviceDomain.Name
        };

        await _context.Services.AddAsync(service);
    }

    public async Task DeleteAsync(int serviceId)
    {
        var service = await _context.Services.Where(x => x.Id == serviceId).FirstOrDefaultAsync();

        if (service == null)
            throw new Exception($"Service not found with the given Id {serviceId}");

        service.IsDeleted = true;

        _context.Services.Update(service);
    }

    public async Task<ServiceDisplay?> GetServiceByIdAsync(int serviceId)
    {
        var service = await _context.Services
            .Where(x => x.Id == serviceId)
            .Select(x => new ServiceDisplay
            {
                Id = x.Id,
                Name = x.Name
            })
            .FirstOrDefaultAsync();

        return service;
    }

    public async Task UpdateServiceAsync(UpdateService command)
    {
        var service = await _context.Services.Where(x => x.Id == command.Id).FirstOrDefaultAsync();

        if (service == null)
            throw new Exception($"Service not found with the given Id {command.Id}");

        service.Name = command.Name;

        _context.Services.Update(service);
    }
}