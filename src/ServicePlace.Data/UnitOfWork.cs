using ServicePlace.Model.Contracts;

namespace ServicePlace.Data;

public class UnitOfWork : IUnitOfWork
{
    ServicePlaceContext _context;

    public UnitOfWork(ServicePlaceContext context)
    {
        _context = context;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}