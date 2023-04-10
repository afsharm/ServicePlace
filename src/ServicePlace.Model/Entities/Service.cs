using System.ComponentModel.DataAnnotations;

namespace ServicePlace.Model.Entities;
public class Service
{
    public int Id { get; set; }

    [MaxLength(100)]
    public string? Name { get; set; }
}
