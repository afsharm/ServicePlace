using Microsoft.EntityFrameworkCore;
using ServicePlace.Data;

namespace ServicePlace.UnitTest.Common;

public class TestDatabaseFixture
{
    private const string ConnectionString = @"Data Source=ServicePlaceTest.db";

    private static readonly object _lock = new();
    private static bool _databaseInitialized;

    public TestDatabaseFixture()
    {
        lock (_lock)
        {
            if (!_databaseInitialized)
            {
                using (var context = CreateContext())
                {
                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();

                    context.AddRange(
                        new ServicePlace.Data.Entities.Service { Name = "Service 1" },
                        new ServicePlace.Data.Entities.Service { Name = "Service 2" });
                    context.SaveChanges();
                }

                _databaseInitialized = true;
            }
        }
    }

    public ServicePlaceContext CreateContext()
        => new ServicePlaceContext(
            new DbContextOptionsBuilder<ServicePlaceContext>()
                .UseSqlite(ConnectionString)
                .Options);
}