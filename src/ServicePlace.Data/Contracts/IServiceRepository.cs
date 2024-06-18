using ServicePlace.Model.Commands;
using ServicePlace.Data.Entities;
using ServicePlace.Model.Queries;

namespace ServicePlace.Data.Contracts;

public interface IServiceRepository
{
    Task<IEnumerable<ServiceDisplay>> GetServicesAsync();
    Task AddAsync(Service service);
    Task DeleteAsync(int serviceId);
    Task<ServiceDisplay?> GetServiceByIdAsync(int serviceId);
    Task UpdateServiceAsync(UpdateService command);
}