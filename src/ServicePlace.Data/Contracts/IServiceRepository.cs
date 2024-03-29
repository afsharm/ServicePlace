using ServicePlace.Model.Entities;
using ServicePlace.Model.Queries;

namespace ServicePlace.Data.Contracts;

public interface IServiceRepository
{
    Task<IEnumerable<ServiceDisplay>> GetServicesAsync();
    Task AddAsync(Service service);
}