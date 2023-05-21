using Microsoft.Extensions.Logging;
using Moq;
using ServicePlace.Model.Commands;
using ServicePlace.Service;
using ServicePlace.Web.Controllers;

[Collection("TransactionalTests")]
public class ServiceControllerTest : IClassFixture<TestDatabaseFixture>
{
    public ServiceControllerTest(TestDatabaseFixture fixture)
        => Fixture = fixture;

    public TestDatabaseFixture Fixture { get; }

    [Fact]
    public async Task GetServiceTest()
    {
        //Arrange
        using var context = Fixture.CreateContext();
        var loggerCommonService = Mock.Of<ILogger<CommonService>>();
        var commonService = new CommonService(context, loggerCommonService);
        var loggerServiceController = Mock.Of<ILogger<ServiceController>>();
        var serviceController = new ServiceController(loggerServiceController, commonService, context);

        //Act
        var exception = await Record.ExceptionAsync(() => serviceController.GetServicesAsync());

        //Assert
        Assert.Null(exception);
    }

    [Fact]
    public async Task GetService_not_null()
    {
        //Arrange
        using var context = Fixture.CreateContext();
        var loggerCommonService = Mock.Of<ILogger<CommonService>>();
        var commonService = new CommonService(context, loggerCommonService);
        var loggerServiceController = Mock.Of<ILogger<ServiceController>>();
        var serviceController = new ServiceController(loggerServiceController, commonService, context);

        //Act
        var result = await serviceController.GetServicesAsync();

        //Assert
        Assert.NotNull(result);
    }

    [Theory]
    [InlineData("0")]
    [InlineData("1")]
    [InlineData("5")]
    [InlineData("12")]
    [InlineData("43")]
    [InlineData("183")]
    public async Task GetService_increases(string value)
    {
        //Arrange
        using var context = Fixture.CreateContext();
        var loggerCommonService = Mock.Of<ILogger<CommonService>>();
        var commonService = new CommonService(context, loggerCommonService);
        var loggerServiceController = Mock.Of<ILogger<ServiceController>>();
        var serviceController = new ServiceController(loggerServiceController, commonService, context);

        //Act
        var resultBefore = await serviceController.GetServicesAsync();
        var countBefore = resultBefore.Count();

        var limit = int.Parse(value);

        for (var i = 0; i < limit; i++)
        {
            var createService = new CreateService { Name = Guid.NewGuid().ToString() };
            await serviceController.CreateServiceAsync(createService);
        }

        var resultAfter = await serviceController.GetServicesAsync();
        var countAfter = resultAfter.Count();
        var countDiff = countAfter - countBefore;

        //Assert
        Assert.Equal(limit, countDiff);
    }
}