using AutoMapper;
using Blazored.LocalStorage;
using HR.LeaveManagement.BlazorUI.Contracts;
using HR.LeaveManagement.BlazorUI.Models.LeaveAllocations;
using HR.LeaveManagement.BlazorUI.Models.LeaveRequests;
using HR.LeaveManagement.BlazorUI.Services.Base;

namespace HR.LeaveManagement.BlazorUI.Services;

public class LeaveRequestService : BaseHttpService, ILeaveRequestService
{
    private readonly IMapper _mapper;

    public LeaveRequestService(IServiceClient serviceClient, ILocalStorageService localStorageService, IMapper mapper) : base(serviceClient, localStorageService)
    {
        _mapper = mapper;
    }

    public async Task<AdminLeaveRequestViewVM> GetAdminLeaveRequestList()
    {
        var leaveRequests = await ServiceClient.LeaveRequestsAllAsync(false);
        
        var model = new AdminLeaveRequestViewVM()
        {
            TotalRequests = leaveRequests.Count,
            ApprovedRequests = leaveRequests.Count(q => q.Approved == true),
            RejectedRequests = leaveRequests.Count(q => q.Approved == false),
            PendingRequests = leaveRequests.Count(q => q.Approved == null),
            LeaveRequests = _mapper.Map<List<LeaveRequestVM>>(leaveRequests)
        };
        return model;
    }

    public async Task<EmployeeLeaveRequestViewVM> GetUserLeaveRequests()
    {
        var leaveRequests = await ServiceClient.LeaveRequestsAllAsync(isLoggedInUser: true);
        var allocations = await ServiceClient.LeaveAllocationsAllAsync();
        var model = new EmployeeLeaveRequestViewVM
        {
            LeaveAllocations = _mapper.Map<List<LeaveAllocationVM>>(allocations),
            LeaveRequests = _mapper.Map<List<LeaveRequestVM>>(leaveRequests)
        };

        return model;
    }

    public async Task<LeaveRequestVM> GetLeaveRequest(int id)
    {
        var leaveRequest = await ServiceClient.LeaveRequestsGETAsync(id);
        return _mapper.Map<LeaveRequestVM>(leaveRequest);    
    }

    public async Task<Response<Guid>> ApproveLeaveRequest(int id, bool approved)
    {
        try
        {
            var response = new Response<Guid>();
            var request = new ChangeLeaveRequestApprovalCommand { Approved = approved, Id = id };
            await ServiceClient.UpdateApprovalAsync(request);
            return response;
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }

    public async Task<Response<Guid>> CreateLeaveRequest(LeaveRequestVM leaveRequest)
    {
        try
        {
            var response = new Response<Guid>();
            CreateLeaveRequestCommand createLeaveRequest = _mapper.Map<CreateLeaveRequestCommand>(leaveRequest);

            await ServiceClient.LeaveRequestsPOSTAsync(createLeaveRequest);
            return response;
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }    
    }

    public async Task<Response<Guid>> CancelLeaveRequest(int id)
    {
        try
        {
            var response = new Response<Guid>();
            var request = new CancelLeaveRequestCommand { Id = id };
            await ServiceClient.CancelRequestAsync(request);
            return response;
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }
}