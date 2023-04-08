using ServicePlace.Data;
using ServicePlace.Model;
using Microsoft.EntityFrameworkCore;

namespace ServicePlace.Service;
public class AppService
{
    private readonly ServicePlaceContext _context;

    public AppService(ServicePlaceContext context)
    {
        _context = context;
    }

    public IEnumerable<Provider> GetAllProvider()
    {
        return _context.Providers
            .AsNoTracking()
            .ToList();
    }
}
