using ServicePlace.Model.Queries;

namespace ServicePlace.Data.Repositories;

public interface IProviderRepository
{
    Task<IEnumerable<ProviderDisplay>> GetAllProvidersAsync();
}