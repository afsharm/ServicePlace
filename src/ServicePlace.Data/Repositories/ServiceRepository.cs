using Microsoft.EntityFrameworkCore;
using ServicePlace.Model.Queries;
using ServicePlace.Model.Contracts;
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
}