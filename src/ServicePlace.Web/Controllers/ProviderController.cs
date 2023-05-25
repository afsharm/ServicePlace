using Microsoft.AspNetCore.Mvc;
using ServicePlace.Model;
using ServicePlace.Model.Commands;
using ServicePlace.Service.Contracts;
using ServicePlace.Model.Queries;
using ServicePlace.Model.Results;
using ServicePlace.Data.Contracts;

namespace ServicePlace.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class ProviderController : ControllerBase
{
    private readonly ILogger<ProviderController> _logger;
    private readonly ICommonService _commonService;
    private readonly IUnitOfWork _unitOfWork;

    public ProviderController(ILogger<ProviderController> logger, ICommonService commonService, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _commonService = commonService;
        _unitOfWork = unitOfWork;
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
            await _unitOfWork.SaveChangesAsync();
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

    [HttpPost]
    public async Task<CreateProviderResult> CreateProviderAsync(CreateProviderCommand? command)
    {
        try
        {
            var result = await _commonService.CreateProviderAsync(command);

            return result;
        }
        catch (System.Exception)
        {
            throw;
        }
    }
}
