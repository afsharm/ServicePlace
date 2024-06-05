using ServicePlace.Model.Commands;
using ServicePlace.Model.Queries;
using ServicePlace.Model.Results;

namespace ServicePlace.Service.Contracts;

public interface ICommonService
{
    Task<IEnumerable<ProviderDisplay>> GetAllProvidersAsync();
    Task<IEnumerable<ServiceDisplay>> GetServicesAsync();
    Task<CreateServiceResult> CreateServiceAsync(CreateService command);
    Task<IEnumerable<ProviderDisplay>> GetProviderByServiceIdAsync(int serviceId);
    Task UpdateProviderAsync(UpdateProvider command);
    Task<CreateProviderResult> CreateProviderAsync(CreateProviderCommand? command);
    Task DeleteServiceAsync(int serviceId);
    Task<ServiceDisplay?> GetServiceByIdAsync(int serviceId);
    Task UpdateServiceAsync(UpdateService command);
    Task<ProviderDisplay?> GetProviderByIdAsync(int providerId);
}