namespace ServicePlace.Core.Commands;

public class CreateProviderCommand
{
    public Guid? ServiceId { get; set; }
    public string? Name { get; set; }
}