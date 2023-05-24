using ServicePlace.Model.Entities;
using ServicePlace.Model.Queries;

namespace ServicePlace.Data.Contracts;

public interface IProviderRepository
{
    Task<IEnumerable<ProviderDisplay>> GetAllProvidersAsync();
    Task<IEnumerable<ProviderDisplay>> GetProviderByServiceIdAsync(int serviceId);
    Task<Provider?> GetProviderAsync(int id);
    void UpdateProvider(Provider provider);
    Task<bool> AnyDuplicateAsync(string? name, int? serviceId);
    Task AddProviderAsync(Provider newProvider);
}