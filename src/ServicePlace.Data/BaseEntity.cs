public abstract class BaseDatabaseEntity
{
    public Guid Id { get; set; }
    public bool IsDeleted { get; set; }
}