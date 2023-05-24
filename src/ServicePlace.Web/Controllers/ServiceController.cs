using Microsoft.AspNetCore.Mvc;
using ServicePlace.Data;
using ServicePlace.Model.Commands;
using ServicePlace.Model.Contracts;
using ServicePlace.Model.Queries;
using ServicePlace.Model.Results;

namespace ServicePlace.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class ServiceController : ControllerBase
{
    private readonly ILogger<ServiceController> _logger;
    private readonly ICommonService _commonService;
    private readonly ServicePlaceContext _context;

    public ServiceController(ILogger<ServiceController> logger, ICommonService commonService, ServicePlaceContext context)
    {
        _logger = logger;
        _commonService = commonService;
        _context = context;
    }

    [HttpGet]
    public async Task<IEnumerable<ServiceDisplay>> GetServicesAsync()
    {
        var response = await _commonService.GetServicesAsync();
        return response;
    }

    [HttpPost]
    public async Task<CreateServiceResult> CreateServiceAsync(CreateService command)
    {
        var result = await _commonService.CreateServiceAsync(command);

        //SaveChanges is called in the service layer directly in order to get the DB generated Id

        return result;
    }
}
