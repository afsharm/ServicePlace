namespace ServicePlace.Model.Contracts;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}