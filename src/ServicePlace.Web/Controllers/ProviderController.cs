using Microsoft.AspNetCore.Mvc;
using ServicePlace.Model;
using ServicePlace.Model.Commands;
using ServicePlace.Core.Contracts;
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
    public async Task<PagingResult<ProviderDisplay>> GetAllProvidersAsync([FromQuery] ProviderPagingQuery query)
    {
        return await _commonService.GetAllProvidersAsync(query);
    }

    [HttpGet("{providerId}")]
    public async Task<ProviderDisplay?> GetProviderAsync(int providerId)
    {
        return await _commonService.GetProviderByIdAsync(providerId);
    }

    [HttpGet("byServiceId/{serviceId}")]
    public async Task<IEnumerable<ProviderDisplay>> GetProviderByServiceIdAsync(int serviceId)
    {
        return await _commonService.GetProviderByServiceIdAsync(serviceId);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync(UpdateProvider command)
    {
        try
        {
            await _commonService.UpdateProviderAsync(command);
            await _unitOfWork.SaveChangesAsync();
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
        catch (Exception)
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

    [HttpDelete]
    public async Task<IActionResult> DeleteProviderAsync([FromQuery] int providerId)
    {
        await _commonService.DeleteProviderAsync(providerId);

        await _unitOfWork.SaveChangesAsync();

        return NoContent();
    }
}
