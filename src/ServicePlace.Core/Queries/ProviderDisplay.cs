
namespace ServicePlace.Core.Queries;

public class ProviderDisplay
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public Guid ServiceId { get; set; }
    public string? ServiceName { get; set; }
}