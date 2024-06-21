using ServicePlace.Core.DomainEntities;
using ServicePlace.Core.Queries;
using ServicePlace.Core.Results;

namespace ServicePlace.Core.Contracts;

public interface IProviderRepository
{
    Task<PagingResult<ProviderDisplay>> GetAllProvidersAsync(ProviderPagingQuery query);
    Task<IEnumerable<ProviderDisplay>> GetProviderByServiceIdAsync(int serviceId);
    Task<Provider?> GetProviderAsync(int id);
    void UpdateProvider(Provider provider);
    Task<bool> AnyDuplicateAsync(string? name, int? serviceId);
    Task AddProviderAsync(Provider newProvider);
    Task<ProviderDisplay?> GetProviderByIdAsync(int providerId);
    Task DeleteAsync(int providerId);
}