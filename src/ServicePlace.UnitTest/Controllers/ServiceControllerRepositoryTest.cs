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
    private ServiceController BuildServiceController(IServiceRepository serviceRepository = null!)
    {
        if (serviceRepository == null)
            serviceRepository = Mock.Of<IServiceRepository>();

        ICommonService commonService = new CommonService(Mock.Of<ILogger<CommonService>>(),
            serviceRepository, Mock.Of<IProviderRepository>(), Mock.Of<IUnitOfWork>());
        var serviceController = new ServiceController(Mock.Of<ILogger<ServiceController>>(), commonService);
        return serviceController;
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
        var serviceRepositoryMock = new Mock<IServiceRepository>();
        serviceRepositoryMock.Setup(x => x.GetServicesAsync().Result).Returns(new List<ServiceDisplay>() { new ServiceDisplay() });
        var serviceController = BuildServiceController(serviceRepositoryMock.Object);

        //Action
        var list = await serviceController.GetServicesAsync();

        //Assert
        Assert.Equal(1, list.Count());
    }
}
