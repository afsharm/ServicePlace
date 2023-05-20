namespace ServicePlace.Model.Commands;

public class CreateProviderCommand
{
    public int? ServiceId { get; set; }
    public string? Name { get; set; }
}