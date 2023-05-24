using ServicePlace.Service;
using ServicePlace.Model.Commands;
using Moq;
using Microsoft.Extensions.Logging;
using ServicePlace.Data.Repositories;

namespace ServicePlace.UnitTest;

[Collection("TransactionalTests")]
public class CommonServiceTestBasic : IClassFixture<TestDatabaseFixture>
{
    public CommonServiceTestBasic(TestDatabaseFixture fixture)
        => Fixture = fixture;

    public TestDatabaseFixture Fixture { get; }

    [Fact]
    public async Task Validate_create_should_not_throw_exception_for_ABC_string()
    {
        //Arrange
        using var context = Fixture.CreateContext();
        var logger = Mock.Of<ILogger<CommonService>>();
        var serviceRepository = new ServiceRepository(context);
        var commonService = new CommonService(context, logger, serviceRepository);

        //Act
        var createService = new CreateService
        {
            Name = "ABC"
        };

        //Assert
        var exception = await Record.ExceptionAsync(() => commonService.CreateServiceAsync(createService));
        Assert.Null(exception);
    }

    [Theory]
    [InlineData("String101abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyz01234567890123")]
    [InlineData("String104abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyz01234567890123456")]
    public async Task different_invalid_items_are_not_allowed(string value)
    {
        //Arrange
        using var context = Fixture.CreateContext();
        var logger = Mock.Of<ILogger<CommonService>>();
        var serviceRepository = new ServiceRepository(context);
        var commonService = new CommonService(context, logger, serviceRepository);

        //Act
        var createService = new CreateService
        {
            Name = value
        };

        //Assert
        await Assert.ThrowsAsync<Exception>(() => commonService.CreateServiceAsync(createService));
    }
}