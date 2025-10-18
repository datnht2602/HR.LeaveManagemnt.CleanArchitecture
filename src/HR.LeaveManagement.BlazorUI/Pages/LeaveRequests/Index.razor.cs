using HR.LeaveManagement.BlazorUI.Contracts;
using HR.LeaveManagement.BlazorUI.Models.LeaveRequests;
using Microsoft.AspNetCore.Components;

namespace HR.LeaveManagement.BlazorUI.Pages.LeaveRequests;

public partial class Index : ComponentBase
{
    [Inject]
    NavigationManager _navManager { get; set; }
    
    [Inject]
    ILeaveRequestService _leaveRequestService { get; set; }

    public AdminLeaveRequestViewVM Model { get; set; } = new();
    
    protected override async Task OnInitializedAsync()
    {
        Model = await _leaveRequestService.GetAdminLeaveRequestList();
    }
    
    void GoToDetails(int id)
    {
        _navManager.NavigateTo($"/leaverequests/details/{id}");
    }
}