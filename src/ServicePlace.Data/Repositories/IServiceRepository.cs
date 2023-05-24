using ServicePlace.Model.Queries;

namespace ServicePlace.Data.Repositories;

public interface IServiceRepository
{
    Task<IEnumerable<ServiceDisplay>> GetServicesAsync();
}