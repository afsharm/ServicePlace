using ServicePlace.Core.Commands;
using ServicePlace.Core.DomainEntities;
using ServicePlace.Core.Queries;

namespace ServicePlace.Core.Contracts;

public interface IServiceRepository
{
    Task<IEnumerable<ServiceDisplay>> GetServicesAsync();
    Task AddAsync(ServiceDomain service);
    Task DeleteAsync(Guid serviceId);
    Task<ServiceDisplay?> GetServiceByIdAsync(Guid serviceId);
    Task UpdateServiceAsync(UpdateService command);
}