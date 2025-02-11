using System.ComponentModel.DataAnnotations;

namespace ServicePlace.Data.DatabaseEntities;
public class Provider: BaseDatabaseEntity
{
    [MaxLength(100)]
    public string? Name { get; set; }

    public Service Service { get; set; } = null!;

    public Guid ServiceId { get; set; }
}
