using ServicePlace.Model.Commands;
using ServicePlace.Model.Queries;
using ServicePlace.Model.Results;

namespace ServicePlace.Service;

public interface ICommonService
{
    Task<IEnumerable<ProviderDisplay>> GetAllProvidersAsync();
    Task<IEnumerable<ServiceDisplay>> GetServicesAsync();
    Task<CreateServiceResult> CreateServiceAsync(CreateService command);
    Task<IEnumerable<ProviderDisplay>> GetProviderByServiceIdAsync(int serviceId);
    Task UpdateProviderAsync(int id, string name);
    Task<CreateProviderResult> CreateProviderAsync(CreateProviderCommand? command);
}