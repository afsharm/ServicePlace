using Microsoft.Extensions.Logging;
using Moq;
using ServicePlace.Model.Commands;
using ServicePlace.Service;
using ServicePlace.Web.Controllers;

[CollectionDefinition("TransactionalTests")]
public class ProviderControllerTest : IClassFixture<TestDatabaseFixture>
{
    public ProviderControllerTest(TestDatabaseFixture fixture)
        => Fixture = fixture;

    public TestDatabaseFixture Fixture { get; }

    private ProviderController BuildProviderController()
    {
        using var context = Fixture.CreateContext();
        var loggerService = Mock.Of<ILogger<CommonService>>();
        var commonService = new CommonService(context, loggerService);
        var loggerController = Mock.Of<ILogger<ProviderController>>();
        var controller = new ProviderController(loggerController, commonService, context);

        return controller;
    }

    private (ServiceController Service, ProviderController Provider) BuildProviderAndServiceController()
    {
        using var context = Fixture.CreateContext();
        var loggerService = Mock.Of<ILogger<CommonService>>();
        var commonService = new CommonService(context, loggerService);
        var loggerProviderController = Mock.Of<ILogger<ProviderController>>();
        var loggerServiceController = Mock.Of<ILogger<ServiceController>>();
        var providerController = new ProviderController(loggerProviderController, commonService, context);
        var serviceController = new ServiceController(loggerServiceController, commonService, context);

        return (serviceController, providerController);
    }

    [Fact]
    public async Task CreateProvider_does_not_throw_exception_on_basic_conditions()
    {
        //Arrange
        var controller = BuildProviderController();

        //Action
        var exception = await Record.ExceptionAsync(() => controller.CreateProviderAsync(new CreateProviderCommand { ServiceId = 1, Name = "Provider ABC" }));

        //Assert
        Assert.Null(exception);
    }

    [Fact]
    public async Task CreateProvider_throws_exception_when_command_is_null()
    {
        //Arrange
        var controller = BuildProviderController();

        //Action
        var exception = await Record.ExceptionAsync(() => controller.CreateProviderAsync(null));

        //Assert
        Assert.NotNull(exception);
    }

    [Fact]
    public async Task CreateProvider_throws_exception_when_serviceId_is_null_or_empty()
    {
        //Arrange
        var controller = BuildProviderController();

        //Action
        var exception = await Record.ExceptionAsync(() => controller.CreateProviderAsync(new CreateProviderCommand { ServiceId = null, Name = "ABC" }));

        //Assert
        Assert.NotNull(exception);
    }

    [Theory]
    [InlineData("-1")]
    [InlineData("0")]
    public async Task CreateProvider_invalid_serviceIds_are_not_allowed(string value)
    {
        //Arrange
        var controller = BuildProviderController();

        //Action
        var serivceId = Convert.ToInt32(value);
        var exception = await Record.ExceptionAsync(() => controller.CreateProviderAsync(new CreateProviderCommand { ServiceId = serivceId, Name = "ABC" }));

        //Assert
        Assert.NotNull(exception);
    }

    [Fact]
    public async Task CreateProvider_simple_creation_works()
    {
        //Arrange
        var controllers = BuildProviderAndServiceController();
        var result = await controllers.Service.CreateServiceAsync(new CreateService { Name = Guid.NewGuid().ToString() });
        var createProviderCommand = new CreateProviderCommand { ServiceId = result.ServiceId, Name = Guid.NewGuid().ToString() };

        //Action
        var exception = await Record.ExceptionAsync(() => controllers.Provider.CreateProviderAsync(createProviderCommand));

        //Assert
        Assert.Null(exception);
    }

    [Theory]
    [InlineData("Best Washers")]
    [InlineData("Fire fighters")]
    [InlineData("Sky line")]
    [InlineData("Dr. Brown")]
    [InlineData("Street Beauty")]
    [InlineData("012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789")]
    public async Task CreateProvider_allowed_provider_names_work(string value)
    {
        //Arrange
        var controllers = BuildProviderAndServiceController();
        var result = await controllers.Service.CreateServiceAsync(new CreateService { Name = Guid.NewGuid().ToString() });
        var createProviderCommand = new CreateProviderCommand { ServiceId = result.ServiceId, Name = value };

        //Action
        var exception = await Record.ExceptionAsync(() => controllers.Provider.CreateProviderAsync(createProviderCommand));

        //Assert
        Assert.Null(exception);
    }

    [Theory]
    [InlineData("012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789x")]
    [InlineData(" ")]
    [InlineData("  ")]
    [InlineData("        ")]
    [InlineData("ab")]
    [InlineData("c")]
    public async Task CreateProvider_disallowed_provider_names_should_not_be_allowed(string value)
    {
        //Arrange
        var controllers = BuildProviderAndServiceController();
        var result = await controllers.Service.CreateServiceAsync(new CreateService { Name = Guid.NewGuid().ToString() });
        var createProviderCommand = new CreateProviderCommand { ServiceId = result.ServiceId, Name = value };

        //Action
        var exception = await Record.ExceptionAsync(() => controllers.Provider.CreateProviderAsync(createProviderCommand));

        //Assert
        Assert.NotNull(exception);
    }

    [Fact]
    public async Task CreateProvider_dupes_should_not_be_allowed_within_same_service()
    {
        //Arrange
        var controllers = BuildProviderAndServiceController();
        var result = await controllers.Service.CreateServiceAsync(new CreateService { Name = Guid.NewGuid().ToString() });
        var name = Guid.NewGuid().ToString();
        var createProviderCommand = new CreateProviderCommand { ServiceId = result.ServiceId, Name = name };
        await controllers.Provider.CreateProviderAsync(createProviderCommand);

        //Action
        var exception = await Record.ExceptionAsync(() => controllers.Provider.CreateProviderAsync(createProviderCommand));

        //Assert
        Assert.NotNull(exception);
    }

    [Fact]
    public async Task CreateProvider_dupes_should_be_allowed_within_different_services()
    {
        //Arrange
        var controllers = BuildProviderAndServiceController();
        var firstResult = await controllers.Service.CreateServiceAsync(new CreateService { Name = Guid.NewGuid().ToString() });
        var secondResult = await controllers.Service.CreateServiceAsync(new CreateService { Name = Guid.NewGuid().ToString() });
        var name = Guid.NewGuid().ToString();
        var createProviderCommandFirst = new CreateProviderCommand { ServiceId = firstResult.ServiceId, Name = name };
        await controllers.Provider.CreateProviderAsync(createProviderCommandFirst);
        var createProviderCommandSecond = new CreateProviderCommand { ServiceId = secondResult.ServiceId, Name = name };

        //Action
        var exception = await Record.ExceptionAsync(() => controllers.Provider.CreateProviderAsync(createProviderCommandSecond));

        //Assert
        Assert.Null(exception);
    }
}