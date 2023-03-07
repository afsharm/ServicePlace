using System.ComponentModel.DataAnnotations;

namespace ServicePlace.Model;
public class Provider
{
    public int Id { get; set; }

    [MaxLength(100)]
    public string? Name { get; set; }
}
