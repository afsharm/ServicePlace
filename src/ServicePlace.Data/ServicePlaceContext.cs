using Microsoft.EntityFrameworkCore;
using ServicePlace.Model;

namespace ServicePlace.Data;
public class ServicePlaceContext : DbContext
{
    public ServicePlaceContext(DbContextOptions<ServicePlaceContext> options)
        : base(options)
    {
    }

    public DbSet<Provider> Providers => Set<Provider>();
}