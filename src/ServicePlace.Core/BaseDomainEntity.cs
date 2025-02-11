public abstract class BaseDomainEntity
{
    public BaseDomainEntity()
        => Id = Guid.NewGuid();
        
    public Guid Id { get; set; }
}