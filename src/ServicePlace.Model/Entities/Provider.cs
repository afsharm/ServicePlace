﻿using System.ComponentModel.DataAnnotations;

namespace ServicePlace.Model.Entities;
public class Provider
{
    public int Id { get; set; }

    [MaxLength(100)]
    public string? Name { get; set; }

    public Service Service { get; set; } = null!;

    public int ServiceId { get; set; }
    public bool IsDeleted { get; set; }
}
