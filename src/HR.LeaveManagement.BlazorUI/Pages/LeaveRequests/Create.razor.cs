using HR.LeaveManagement.BlazorUI.Contracts;
using HR.LeaveManagement.BlazorUI.Models.LeaveRequests;
using HR.LeaveManagement.BlazorUI.Models.LeaveTypes;
using Microsoft.AspNetCore.Components;

namespace HR.LeaveManagement.BlazorUI.Pages.LeaveRequests;

public partial class Create : ComponentBase
{

    [Inject] 
    private ILeaveTypeService _leaveTypeService { get; set; }
    
    [Inject] 
    private ILeaveRequestService _leaveRequestService { get; set; }
    
    [Inject] 
    private NavigationManager _navigationManager { get; set; }
    
    LeaveRequestVM LeaveRequest { get; set; } = new();
    
    List<LeaveTypeVM> LeaveTypeVMs { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        LeaveTypeVMs = await _leaveTypeService.GetLeaveTypes();
    }

    private async Task HandleValidSubmit()
    {
        await _leaveRequestService.CreateLeaveRequest(LeaveRequest);
        _navigationManager.NavigateTo("/leaverequests/");
    }
}