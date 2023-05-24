namespace ServicePlace.Data.Contracts;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}