using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using ServicePlace.Data;
using ServicePlace.Data.Repositories;
using ServicePlace.Model.Commands;
using ServicePlace.Data.Contracts;
using ServicePlace.Service.Contracts;
using ServicePlace.Service;
using ServicePlace.Web.Controllers;
using ServicePlace.UnitTest.Common;

namespace ServicePlace.UnitTest.Controllers;

[Collection("TransactionalTests")]
public class ProviderControllerTest : IClassFixture<TestDatabaseFixture>
{
    public ProviderControllerTest(TestDatabaseFixture fixture)
        => Fixture = fixture;

    public TestDatabaseFixture Fixture { get; }

    private ProviderController BuildProviderController(ServicePlaceContext context)
    {
        var loggerService = Mock.Of<ILogger<CommonService>>();
        IServiceRepository serviceRepository = new ServiceRepository(context);
        IProviderRepository providerRepository = new ProviderRepository(context);
        IUnitOfWork unitOfWork = new UnitOfWork(context);
        ICommonService commonService = new CommonService(loggerService, serviceRepository, providerRepository, unitOfWork);
        var loggerController = Mock.Of<ILogger<ProviderController>>();
        var controller = new ProviderController(loggerController, commonService, unitOfWork);

        return controller;
    }

    private (ServiceController Service, ProviderController Provider) BuildProviderAndServiceController(ServicePlaceContext context)
    {
        var loggerService = Mock.Of<ILogger<CommonService>>();
        IServiceRepository serviceRepository = new ServiceRepository(context);
        IProviderRepository providerRepository = new ProviderRepository(context);
        IUnitOfWork unitOfWork = new UnitOfWork(context);
        ICommonService commonService = new CommonService(loggerService, serviceRepository, providerRepository, unitOfWork);
        var loggerProviderController = Mock.Of<ILogger<ProviderController>>();
        var loggerServiceController = Mock.Of<ILogger<ServiceController>>();
        var providerController = new ProviderController(loggerProviderController, commonService, unitOfWork);
        var serviceController = new ServiceController(loggerServiceController, commonService, Mock.Of<IUnitOfWork>());

        return (serviceController, providerController);
    }

    [Fact]
    public async Task create_provider_does_not_throw_exception_on_basic_conditions()
    {
        //Arrange
        using var context = Fixture.CreateContext();
        var controller = BuildProviderController(context);

        //Action
        var exception = await Record.ExceptionAsync(() => controller.CreateProviderAsync(new CreateProviderCommand { ServiceId = 1, Name = "Provider ABC" }));

        //Assert
        Assert.Null(exception);
    }

    [Fact]
    public async Task create_provider_throw_exception_when_command_is_null()
    {
        //Arrange
        using var context = Fixture.CreateContext();
        var controller = BuildProviderController(context);

        //Action
        var exception = await Record.ExceptionAsync(() => controller.CreateProviderAsync(null));

        //Assert
        Assert.NotNull(exception);
        Assert.Equal(typeof(ArgumentNullException), exception.GetType());
        Assert.Equal("Value cannot be null. (Parameter 'command')", exception.Message);
    }

    [Fact]
    public async Task create_provider_throw_exception_when_serviceId_is_null_or_empty()
    {
        //Arrange
        using var context = Fixture.CreateContext();
        var controller = BuildProviderController(context);

        //Action
        var exception = await Record.ExceptionAsync(() => controller.CreateProviderAsync(new CreateProviderCommand { ServiceId = null, Name = "ABC" }));

        //Assert
        Assert.NotNull(exception);
        Assert.Equal(typeof(ArgumentNullException), exception.GetType());
        Assert.Equal("Value cannot be null. (Parameter 'ServiceId')", exception.Message);
    }

    [Theory]
    [InlineData("-1")]
    [InlineData("0")]
    public async Task create_a_provider_with_invalid_service_id_should_not_work(string value)
    {
        //Arrange
        using var context = Fixture.CreateContext();
        var controller = BuildProviderController(context);

        //Action
        var serivceId = Convert.ToInt32(value);
        var exception = await Record.ExceptionAsync(() => controller.CreateProviderAsync(new CreateProviderCommand { ServiceId = serivceId, Name = "ABC" }));

        //Assert
        Assert.NotNull(exception);
        Assert.Equal(typeof(DbUpdateException), exception.GetType());
        Assert.Equal("An error occurred while saving the entity changes. See the inner exception for details.", exception.Message);
    }

    [Fact]
    public async Task create_a_simple_provider_works()
    {
        //Arrange
        using var context = Fixture.CreateContext();
        var controllers = BuildProviderAndServiceController(context);
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
    [InlineData("0123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789")]
    public async Task while_creating_a_provider_allowed_provider_names_should_be_allowed(string value)
    {
        //Arrange
        using var context = Fixture.CreateContext();
        var controllers = BuildProviderAndServiceController(context);
        var result = await controllers.Service.CreateServiceAsync(new CreateService { Name = Guid.NewGuid().ToString() });
        var createProviderCommand = new CreateProviderCommand { ServiceId = result.ServiceId, Name = value };

        //Action
        var exception = await Record.ExceptionAsync(() => controllers.Provider.CreateProviderAsync(createProviderCommand));

        //Assert
        Assert.Null(exception);
    }

    [Theory]
    [InlineData("0123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789x",
        "Argument should not be smaller than 3 or bigger than 100 characters (Parameter 'name')")]
    [InlineData(" ", "Argument is null or white space (Parameter 'name')")]
    [InlineData("  ", "Argument is null or white space (Parameter 'name')")]
    [InlineData("        ", "Argument is null or white space (Parameter 'name')")]
    [InlineData("ab", "Argument should not be smaller than 3 or bigger than 100 characters (Parameter 'name')")]
    [InlineData("c", "Argument should not be smaller than 3 or bigger than 100 characters (Parameter 'name')")]
    public async Task while_creating_a_provider_disallowed_provider_names_should_not_be_allowed(string providerName, string expectedErrorMessage)
    {
        //Arrange
        using var context = Fixture.CreateContext();
        var controllers = BuildProviderAndServiceController(context);
        var result = await controllers.Service.CreateServiceAsync(new CreateService { Name = Guid.NewGuid().ToString() });
        var createProviderCommand = new CreateProviderCommand { ServiceId = result.ServiceId, Name = providerName };

        //Action
        var exception = await Record.ExceptionAsync(() => controllers.Provider.CreateProviderAsync(createProviderCommand));

        //Assert
        Assert.NotNull(exception);
        Assert.Equal(typeof(ArgumentException), exception.GetType());
        Assert.Equal(expectedErrorMessage, exception.Message);
    }

    [Fact]
    public async Task while_creating_a_provider_duplicate_service_name_should_not_be_allowed_within_same_service()
    {
        //Arrange
        using var context = Fixture.CreateContext();
        var controllers = BuildProviderAndServiceController(context);
        var result = await controllers.Service.CreateServiceAsync(new CreateService { Name = Guid.NewGuid().ToString() });
        var name = Guid.NewGuid().ToString();
        var createProviderCommand = new CreateProviderCommand { ServiceId = result.ServiceId, Name = name };
        await controllers.Provider.CreateProviderAsync(createProviderCommand);

        //Action
        var exception = await Record.ExceptionAsync(() => controllers.Provider.CreateProviderAsync(createProviderCommand));

        //Assert
        Assert.NotNull(exception);
        Assert.Equal("Duplicate service `name`.", exception.Message);
    }

    [Fact]
    public async Task duplicate_service_name_should_be_allowed_within_different_services_while_creating_a_provider()
    {
        //Arrange
        using var context = Fixture.CreateContext();
        var controllers = BuildProviderAndServiceController(context);
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

    [Fact]
    public async Task creating_a_provider_should_not_create_an_extra_service()
    {
        //Arrange
        using var context = Fixture.CreateContext();
        var controllers = BuildProviderAndServiceController(context);

        //Action
        var createService = new CreateService { Name = Guid.NewGuid().ToString() };
        var createServiceResult = await controllers.Service.CreateServiceAsync(createService);
        var existinigServicesBefore = await controllers.Service.GetServicesAsync();
        var createProviderCommand = new CreateProviderCommand { ServiceId = createServiceResult.ServiceId, Name = Guid.NewGuid().ToString() };
        await controllers.Provider.CreateProviderAsync(createProviderCommand);
        var existinigServicesAfter = await controllers.Service.GetServicesAsync();

        //Assert
        Assert.Equal(existinigServicesBefore.Count(), existinigServicesAfter.Count());
    }
}