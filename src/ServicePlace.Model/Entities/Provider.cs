using System.ComponentModel.DataAnnotations;

namespace ServicePlace.Model.Entities;
public class Provider
{
    public Provider()
    {
        Service = new Service();
    }

    public int Id { get; set; }

    [MaxLength(100)]
    public string? Name { get; set; }

    public Service Service { get; set; }
}
