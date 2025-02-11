using ServicePlace.Core.DomainEntities;
using ServicePlace.Core.Queries;
using ServicePlace.Core.Results;

namespace ServicePlace.Core.Contracts;

public interface IProviderRepository
{
    Task<PagingResult<ProviderDisplay>> GetAllProvidersAsync(ProviderPagingQuery query);
    Task<IEnumerable<ProviderDisplay>> GetProviderByServiceIdAsync(Guid serviceId);
    Task<ProviderDomain?> GetProviderAsync(Guid id);
    void UpdateProvider(ProviderDomain provider);
    Task<bool> AnyDuplicateAsync(string? name, Guid? serviceId);
    Task AddProviderAsync(ProviderDomain newProvider);
    Task<ProviderDisplay?> GetProviderByIdAsync(Guid providerId);
    Task DeleteAsync(Guid providerId);
}