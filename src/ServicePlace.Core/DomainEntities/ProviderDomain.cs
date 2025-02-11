namespace ServicePlace.Core.DomainEntities;
public class ProviderDomain: BaseDomainEntity
{
    public string? Name { get; set; }
    public Guid ServiceId { get; set; }
}