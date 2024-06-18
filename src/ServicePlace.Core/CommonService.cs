using ServicePlace.Model.Queries;
using ServicePlace.Model.Commands;
using ServicePlace.Model;
using Microsoft.Extensions.Logging;
using ServicePlace.Model.Results;
using ServicePlace.Model.Constants;
using ServicePlace.Data.Contracts;
using ServicePlace.Core.Contracts;

namespace ServicePlace.Core;

public class CommonService : ICommonService
{
    private readonly ILogger<CommonService> _logger;
    private readonly IServiceRepository _serviceRepository;
    private readonly IProviderRepository _providerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CommonService(ILogger<CommonService> logger, IServiceRepository serviceRepository, IProviderRepository providerRepository, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _serviceRepository = serviceRepository;
        _providerRepository = providerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<PagingResult<ProviderDisplay>> GetAllProvidersAsync(ProviderPagingQuery query)
    {
        return await _providerRepository.GetAllProvidersAsync(query);
    }

    public async Task<IEnumerable<ServiceDisplay>> GetServicesAsync()
    {
        return await _serviceRepository.GetServicesAsync();
    }

    public async Task<CreateServiceResult> CreateServiceAsync(CreateService command)
    {
        ValidateCreateService(command);
        var service = new Data.Entities.Service
        {
            Name = command.Name
        };
        await _serviceRepository.AddAsync(service);

        //doing save changes is necessary here because we need to return database generated id without exposing Entities to upper layers
        await _unitOfWork.SaveChangesAsync();

        return new CreateServiceResult
        {
            ServiceId = service.Id
        };
    }

    void ValidateCreateService(CreateService command)
    {
        if (command.Name?.Length > 100)
            throw new Exception();
    }

    public async Task<IEnumerable<ProviderDisplay>> GetProviderByServiceIdAsync(int serviceId)
    {
        return await _providerRepository.GetProviderByServiceIdAsync(serviceId);
    }

    public async Task UpdateProviderAsync(UpdateProvider command)
    {
        _logger.LogDebug($"UpdateProviderAsync => {command.Id}, {command.Name}");
        var provider = await _providerRepository.GetProviderAsync(command.Id);

        if (provider == null)
            throw new NotFoundException();

        provider.Name = command.Name;
        _providerRepository.UpdateProvider(provider);
    }

    public async Task<CreateProviderResult> CreateProviderAsync(CreateProviderCommand? command)
    {
        if (command == null)
            throw new ArgumentNullException(nameof(command));

        if (command.ServiceId == null)
            throw new ArgumentNullException(nameof(command.ServiceId));

        ValidateProviderName(command.Name);

        var anyDuplicate = await _providerRepository.AnyDuplicateAsync(command.Name, command.ServiceId);

        if (anyDuplicate)
            throw new Exception(ErrorMessageConstants.DuplicateServiceName);

        var newProvider = new Data.Entities.Provider { Name = command.Name, ServiceId = command.ServiceId.Value };

        await _providerRepository.AddProviderAsync(newProvider);
        await _unitOfWork.SaveChangesAsync();

        return new CreateProviderResult { ProviderId = newProvider.Id };
    }

    private void ValidateProviderName(string? name)
    {
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(name.Trim()))
            throw new ArgumentException(ErrorMessageConstants.IsNullOrWhiteSpace, nameof(name));

        if (name.Trim().Length != name.Length)
            throw new ArgumentException(ErrorMessageConstants.ShouldNoStartOrEnd, nameof(name));

        if (name.Length < 3 || name.Length > 100)
            throw new ArgumentException(ErrorMessageConstants.ShouldNotBeSmaller, nameof(name));
    }

    public async Task DeleteServiceAsync(int serviceId)
    {
        await _serviceRepository.DeleteAsync(serviceId);
    }

    public async Task<ServiceDisplay?> GetServiceByIdAsync(int serviceId)
    {
        return await _serviceRepository.GetServiceByIdAsync(serviceId);
    }

    public async Task UpdateServiceAsync(UpdateService command)
    {
        await _serviceRepository.UpdateServiceAsync(command);
    }

    public async Task<ProviderDisplay?> GetProviderByIdAsync(int providerId)
    {
        return await _providerRepository.GetProviderByIdAsync(providerId);
    }

    public async Task DeleteProviderAsync(int providerId)
    {
        await _providerRepository.DeleteAsync(providerId);
    }
}
