using Microsoft.AspNetCore.Mvc;
using ServicePlace.Model.Commands;
using ServicePlace.Service.Contracts;
using ServicePlace.Model.Queries;
using ServicePlace.Model.Results;
using ServicePlace.Data;
using ServicePlace.Data.Contracts;

namespace ServicePlace.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class ServiceController : ControllerBase
{
    private readonly ILogger<ServiceController> _logger;
    private readonly ICommonService _commonService;
    private readonly IUnitOfWork _unitOfWork;

    public ServiceController(ILogger<ServiceController> logger, ICommonService commonService, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _commonService = commonService;
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IEnumerable<ServiceDisplay>> GetServicesAsync()
    {
        var response = await _commonService.GetServicesAsync();
        return response;
    }

    [HttpGet("{serviceId}")]
    public async Task<IActionResult> GetServiceByIdAsync([FromRoute] int serviceId)
    {
        var response = await _commonService.GetServiceByIdAsync(serviceId);
        return Ok(response);
    }

    [HttpPost]
    public async Task<CreateServiceResult> CreateServiceAsync(CreateService command)
    {
        var result = await _commonService.CreateServiceAsync(command);

        //SaveChanges is called in the service layer directly in order to get the DB generated Id

        return result;
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteServiceAsync([FromQuery] int serviceId)
    {
        await _commonService.DeleteServiceAsync(serviceId);

        await _unitOfWork.SaveChangesAsync();

        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateServiceAsync(UpdateService command)
    {
        await _commonService.UpdateServiceAsync(command);

        await _unitOfWork.SaveChangesAsync();

        return NoContent();
    }
}
