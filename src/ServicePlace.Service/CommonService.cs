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

    public IEnumerable<Provider> GetAllProvider()
    {
        return _context.Providers
            .AsNoTracking()
            .ToList();
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

    public async Task CreateServiceAsync(CreateService createService)
    {
        await _context.Services.AddAsync(new Model.Entities.Service
        {
            Name = createService.Name
        });
    }
}
