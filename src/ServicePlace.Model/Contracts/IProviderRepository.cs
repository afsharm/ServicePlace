using ServicePlace.Model.Queries;

namespace ServicePlace.Model.Contracts;

public interface IProviderRepository
{
    Task<IEnumerable<ProviderDisplay>> GetAllProvidersAsync();
}