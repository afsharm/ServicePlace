using Microsoft.Extensions.Logging;
using Moq;
using ServicePlace.Data;
using ServicePlace.Data.Repositories;
using ServicePlace.Core.Commands;
using ServicePlace.Data.Contracts;
using ServicePlace.Core.Contracts;
using ServicePlace.Core;
using ServicePlace.Web.Controllers;
using ServicePlace.UnitTest.Common;

namespace ServicePlace.UnitTest.Controllers;

[Collection("TransactionalTests")]
public class ServiceControllerTest : IClassFixture<TestDatabaseFixture>
{
    public ServiceControllerTest(TestDatabaseFixture fixture)
        => Fixture = fixture;

    public TestDatabaseFixture Fixture { get; }

    private ServiceController BuildServiceController(ServicePlaceContext context)
    {
        var loggerCommonService = Mock.Of<ILogger<CommonService>>();
        IServiceRepository serviceRepository = new ServiceRepository(context);
        IProviderRepository providerRepository = new ProviderRepository(context);
        IUnitOfWork unitOfWork = new UnitOfWork(context);
        ICommonService commonService = new CommonService(loggerCommonService, serviceRepository, providerRepository);
        var loggerServiceController = Mock.Of<ILogger<ServiceController>>();
        var serviceController = new ServiceController(loggerServiceController, commonService, Mock.Of<IUnitOfWork>());
        return serviceController;
    }

    [Fact]
    public async Task GetServiceTest()
    {
        //Arrange
        using var context = Fixture.CreateContext();
        var serviceController = BuildServiceController(context);

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
        var serviceController = BuildServiceController(context);

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
        var serviceController = BuildServiceController(context);

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