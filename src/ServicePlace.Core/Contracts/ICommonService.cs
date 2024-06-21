using ServicePlace.Core.Commands;
using ServicePlace.Core.Queries;
using ServicePlace.Core.Results;

namespace ServicePlace.Core.Contracts;

public interface ICommonService
{
    Task<PagingResult<ProviderDisplay>> GetAllProvidersAsync(ProviderPagingQuery query);
    Task<IEnumerable<ServiceDisplay>> GetServicesAsync();
    Task<CreateServiceResult> CreateServiceAsync(CreateService command);
    Task<IEnumerable<ProviderDisplay>> GetProviderByServiceIdAsync(int serviceId);
    Task UpdateProviderAsync(UpdateProvider command);
    Task<CreateProviderResult> CreateProviderAsync(CreateProviderCommand? command);
    Task DeleteServiceAsync(int serviceId);
    Task<ServiceDisplay?> GetServiceByIdAsync(int serviceId);
    Task UpdateServiceAsync(UpdateService command);
    Task<ProviderDisplay?> GetProviderByIdAsync(int providerId);
    Task DeleteProviderAsync(int providerId);
}