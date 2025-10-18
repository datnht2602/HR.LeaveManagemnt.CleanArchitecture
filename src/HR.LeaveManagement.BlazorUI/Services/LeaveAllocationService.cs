using Blazored.LocalStorage;
using HR.LeaveManagement.BlazorUI.Contracts;
using HR.LeaveManagement.BlazorUI.Services.Base;

namespace HR.LeaveManagement.BlazorUI.Services;

public class LeaveAllocationService : BaseHttpService, ILeaveAllocationService
{
    public LeaveAllocationService(IServiceClient serviceClient, ILocalStorageService localStorageService) : base(serviceClient, localStorageService)
    {
    }

    public async Task<Response<Guid>> CreateLeaveAllocations(int leaveTypeId)
    {
        try
        {
            var response = new Response<Guid>();
            CreateLeaveAllocationCommand allocationCommand = new() { LeaveTypeId = leaveTypeId };
            
            await ServiceClient.LeaveAllocationsPOSTAsync(allocationCommand);
            return response;
        }
        catch (ApiException e)
        {
            return ConvertApiExceptions<Guid>(e);
        }
    }
}