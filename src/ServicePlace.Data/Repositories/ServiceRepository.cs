using Microsoft.EntityFrameworkCore;
using ServicePlace.Model.Queries;
using ServicePlace.Data.Contracts;
using ServicePlace.Model.Entities;

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

    public async Task AddAsync(Service service)
    {
        await _context.Services.AddAsync(service);
    }

    public async Task DeleteAsync(int serviceId)
    {
        var service = await _context.Services.Where(x => x.Id == serviceId).FirstOrDefaultAsync();

        if (service == null)
            throw new Exception($"Service not found with the given Id {serviceId}");

        service.IsDeleted = true;
    }
}