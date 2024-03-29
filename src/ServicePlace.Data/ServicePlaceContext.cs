﻿using Microsoft.EntityFrameworkCore;
using ServicePlace.Model.Entities;

namespace ServicePlace.Data;
public class ServicePlaceContext : DbContext
{
    public ServicePlaceContext(DbContextOptions<ServicePlaceContext> options)
        : base(options)
    {
    }

    public DbSet<Service> Services => Set<Service>();
    public DbSet<Provider> Providers => Set<Provider>();
}