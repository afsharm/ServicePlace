using Microsoft.Extensions.Logging;
using Moq;
using ServicePlace.Service;
using ServicePlace.Web.Controllers;

[CollectionDefinition("TransactionalTests")]
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
}