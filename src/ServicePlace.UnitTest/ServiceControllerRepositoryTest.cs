using Microsoft.Extensions.Logging;
using Moq;
using ServicePlace.Data.Contracts;
using ServicePlace.Model.Commands;
using ServicePlace.Service;
using ServicePlace.Service.Contracts;
using ServicePlace.Web.Controllers;

namespace ServicePlace.UnitTest;

[Collection("TransactionalTests")]
public class ServiceControllerRepositoryTest
{
    private ServiceController BuildServiceController()
    {
        var loggerCommonService = Mock.Of<ILogger<CommonService>>();
        var serviceRepository = Mock.Of<IServiceRepository>();
        var providerRepository = Mock.Of<IProviderRepository>();
        var unitOfWork = Mock.Of<IUnitOfWork>();
        ICommonService commonService = new CommonService(loggerCommonService, serviceRepository, providerRepository, unitOfWork);
        var loggerServiceController = Mock.Of<ILogger<ServiceController>>();
        var serviceController = new ServiceController(loggerServiceController, commonService);
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
        var result = await serviceController.CreateServiceAsync(command);

        //Assert
        Assert.NotNull(result);
        Assert.True(result.ServiceId > 0);
    }
}
