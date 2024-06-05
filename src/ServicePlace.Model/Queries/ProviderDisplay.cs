using ServicePlace.Model.Entities;

namespace ServicePlace.Model.Queries;

public class ProviderDisplay
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int ServiceId { get; set; }
    public string? ServiceName { get; set; }
}