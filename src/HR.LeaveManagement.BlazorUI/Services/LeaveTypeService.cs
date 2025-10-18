using AutoMapper;
using Blazored.LocalStorage;
using HR.LeaveManagement.BlazorUI.Contracts;
using HR.LeaveManagement.BlazorUI.Models.LeaveTypes;
using HR.LeaveManagement.BlazorUI.Services.Base;

namespace HR.LeaveManagement.BlazorUI.Services;

public class LeaveTypeService : BaseHttpService, ILeaveTypeService
{
    private readonly IMapper _mapper;

    public LeaveTypeService(IServiceClient serviceClient, IMapper mapper, ILocalStorageService localStorageService) : base(serviceClient, localStorageService)
    {
        _mapper = mapper;
    }

    public async Task<List<LeaveTypeVM>> GetLeaveTypes()
    {
        var leaveTypes = await ServiceClient.LeaveTypesAllAsync();
        return _mapper.Map<List<LeaveTypeVM>>(leaveTypes);
    }

    public async Task<LeaveTypeVM> GetLeaveTypeDetails(int id)
    {
        var leaveType = await ServiceClient.LeaveTypesGETAsync(id);
        return _mapper.Map<LeaveTypeVM>(leaveType);
    }

    public async Task<Response<Guid>> CreateLeaveType(LeaveTypeVM leaveType)
    {
        try
        {
            var createLeaveTypeCommand = _mapper.Map<CreateLeaveTypeCommand>(leaveType);
            await ServiceClient.LeaveTypesPOSTAsync(createLeaveTypeCommand);
            return new Response<Guid>()
            {
                Success = true
            };
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }

    public async Task<Response<Guid>> UpdateLeaveType(LeaveTypeVM leaveType)
    {
        try
        {
            var updateLeaveTypeCommand = _mapper.Map<UpdateLeaveTypeCommand>(leaveType);
            await ServiceClient.LeaveTypesPUTAsync(updateLeaveTypeCommand);
            return new Response<Guid>()
            {
                Success = true
            };
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }

    public async Task<Response<Guid>> DeleteLeaveType(int id)
    {
        try
        {
            await ServiceClient.LeaveTypesDELETEAsync(id);
            return new Response<Guid>()
            {
                Success = true
            };
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }
}