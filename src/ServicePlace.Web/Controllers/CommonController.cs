using Microsoft.AspNetCore.Mvc;
using ServicePlace.Model.Queries;
using ServicePlace.Service;

namespace ServicePlace.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class CommonController : ControllerBase
{
    private readonly ILogger<CommonController> _logger;
    CommonService _commonService;

    public CommonController(ILogger<CommonController> logger, CommonService commonService)
    {
        _logger = logger;
        _commonService = commonService;
    }

    [HttpGet(Name = "GetServices")]
    public async Task<IEnumerable<ServiceDisplay>> GetServicesAsync()
    {
        var response = await _commonService.GetServicesAsync();
        return response;
    }
}
