using Microsoft.Extensions.Logging;
using Moq;
using ServicePlace.Data.Contracts;
using ServicePlace.Model.Commands;
using ServicePlace.Model.Queries;
using ServicePlace.Service;
using ServicePlace.Service.Contracts;
using ServicePlace.Web.Controllers;

namespace ServicePlace.UnitTest.Controllers;

[Collection("TransactionalTests")]
public class ServiceControllerRepositoryTest
{
    private ServiceController BuildServiceController()
    {
        var loggerCommonService = Mock.Of<ILogger<CommonService>>();
        var serviceRepositoryMock = new Mock<IServiceRepository>();
        serviceRepositoryMock.Setup(x => x.GetServicesAsync()).Returns(GenerateList);
        var providerRepository = Mock.Of<IProviderRepository>();
        var unitOfWork2 = Mock.Of<IUnitOfWork>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        ICommonService commonService = new CommonService(loggerCommonService, serviceRepositoryMock.Object, providerRepository, unitOfWorkMock.Object);
        var loggerServiceController = Mock.Of<ILogger<ServiceController>>();
        var serviceController = new ServiceController(loggerServiceController, commonService);
        return serviceController;
    }

    private Task<IEnumerable<ServiceDisplay>> GenerateList()
    {
        return Task.FromResult<IEnumerable<ServiceDisplay>>(new List<ServiceDisplay>() { new ServiceDisplay() });
    }

    [Fact]
    public async Task create_service_works_on_simple_conditions()
    {
        //Arrange
        var serviceController = BuildServiceController();
        var serviceName = Guid.NewGuid().ToString();
        var command = new CreateService { Name = serviceName };

        //Action
        var exception = await Record.ExceptionAsync(() => serviceController.CreateServiceAsync(command));

        //Assert
        Assert.Null(exception);
    }

    [Fact]
    public async Task get_services_return_mocked_values()
    {
        //Arrange
        var serviceController = BuildServiceController();

        //Action
        var list = await serviceController.GetServicesAsync();

        //Assert
        Assert.Equal(1, list.Count());
    }
}
