using ServicePlace.Core.DomainEntities;
using ServicePlace.Core.Queries;
using ServicePlace.Core.Results;

namespace ServicePlace.Core.Contracts;

public interface IProviderRepository
{
    Task<PagingResult<ProviderDisplay>> GetAllProvidersAsync(ProviderPagingQuery query);
    Task<IEnumerable<ProviderDisplay>> GetProviderByServiceIdAsync(int serviceId);
    Task<ProviderDomain?> GetProviderAsync(int id);
    void UpdateProvider(ProviderDomain provider);
    Task<bool> AnyDuplicateAsync(string? name, int? serviceId);
    Task AddProviderAsync(ProviderDomain newProvider);
    Task<ProviderDisplay?> GetProviderByIdAsync(int providerId);
    Task DeleteAsync(int providerId);
}