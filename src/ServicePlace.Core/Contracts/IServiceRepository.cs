using ServicePlace.Core.Commands;
using ServicePlace.Core.DomainEntities;
using ServicePlace.Core.Queries;

namespace ServicePlace.Core.Contracts;

public interface IServiceRepository
{
    Task<IEnumerable<ServiceDisplay>> GetServicesAsync();
    Task AddAsync(Service service);
    Task DeleteAsync(int serviceId);
    Task<ServiceDisplay?> GetServiceByIdAsync(int serviceId);
    Task UpdateServiceAsync(UpdateService command);
}