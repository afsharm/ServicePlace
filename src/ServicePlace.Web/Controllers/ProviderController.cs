using Microsoft.AspNetCore.Mvc;
using ServicePlace.Data;
using ServicePlace.Model;
using ServicePlace.Model.Queries;
using ServicePlace.Service;

namespace ServicePlace.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class ProviderController : ControllerBase
{
    private readonly ILogger<ProviderController> _logger;
    private readonly CommonService _commonService;
    private readonly ServicePlaceContext _context;

    public ProviderController(ILogger<ProviderController> logger, CommonService commonService, ServicePlaceContext context)
    {
        _logger = logger;
        _commonService = commonService;
        _context = context;
    }

    [HttpGet]
    public async Task<IEnumerable<ProviderDisplay>> GetAllProvidersAsync()
    {
        return await _commonService.GetAllProvidersAsync();
    }

    [HttpGet("{serviceId}")]
    public async Task<IEnumerable<ProviderDisplay>> GetProviderByServiceIdAsync(int serviceId)
    {
        return await _commonService.GetProviderByServiceIdAsync(serviceId);
    }

    [HttpPut("{id}/{name}")]
    public async Task<IActionResult> UpdateAsync(int id, string name)
    {
        try
        {
            await _commonService.UpdateProviderAsync(id, name);
            await _context.SaveChangesAsync();
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
        catch (System.Exception)
        {
            throw;
        }

        return NoContent();
    }
}
