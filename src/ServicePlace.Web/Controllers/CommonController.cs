using Microsoft.AspNetCore.Mvc;
using ServicePlace.Data;
using ServicePlace.Model.Commands;
using ServicePlace.Model.Queries;
using ServicePlace.Service;

namespace ServicePlace.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class CommonController : ControllerBase
{
    private readonly ILogger<CommonController> _logger;
    private readonly CommonService _commonService;
    private readonly ServicePlaceContext _context;

    public CommonController(ILogger<CommonController> logger, CommonService commonService, ServicePlaceContext context)
    {
        _logger = logger;
        _commonService = commonService;
        _context = context;
    }

    [HttpGet(Name = "GetServices")]
    public async Task<IEnumerable<ServiceDisplay>> GetServicesAsync()
    {
        var response = await _commonService.GetServicesAsync();
        return response;
    }

    [HttpPost(Name = "CreateService")]
    public async Task CreateServiceAsync(CreateService command)
    {
        await _commonService.CreateServiceAsync(command);
        await _context.SaveChangesAsync();
    }
}