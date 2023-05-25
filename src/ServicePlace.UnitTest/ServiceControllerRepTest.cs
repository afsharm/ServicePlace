using Microsoft.Extensions.Logging;
using Moq;
using ServicePlace.Data.Contracts;
using ServicePlace.Service;
using ServicePlace.Service.Contracts;
using ServicePlace.Web.Controllers;

namespace ServicePlace.UnitTest;

[Collection("TransactionalTests")]
public class ServiceControllerRepTest
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
}
