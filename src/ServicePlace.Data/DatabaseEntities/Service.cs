using System.ComponentModel.DataAnnotations;

namespace ServicePlace.Data.DatabaseEntities;
public class Service: BaseDatabaseEntity
{

    [MaxLength(100)]
    public string? Name { get; set; }
}
