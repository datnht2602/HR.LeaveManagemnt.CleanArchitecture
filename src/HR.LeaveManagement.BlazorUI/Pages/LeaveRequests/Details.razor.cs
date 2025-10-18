using HR.LeaveManagement.BlazorUI.Contracts;
using HR.LeaveManagement.BlazorUI.Models.LeaveRequests;
using HR.LeaveManagement.BlazorUI.Models.LeaveTypes;
using Microsoft.AspNetCore.Components;

namespace HR.LeaveManagement.BlazorUI.Pages.LeaveRequests;

public partial class Details : ComponentBase
{
    [Inject]
    ILeaveRequestService _leaveRequestService { get; set; }
    
    [Inject]
    NavigationManager _navigationManager { get; set; }

    [Parameter]
    public int id { get; set; }

    private string _className;
    
    private string _headingText;

    LeaveRequestVM _leaveRequestVm = new ();
    protected override async Task OnParametersSetAsync()
    {
        _leaveRequestVm = await _leaveRequestService.GetLeaveRequest(id);
    }

    protected override async Task OnInitializedAsync()
    {
        if (_leaveRequestVm.Approved == null)
        {
            _className = "warning";
            _headingText = "Pending Approval";
        }
        else if (_leaveRequestVm.Approved == true)
        {
            _className = "success";
            _headingText = "Approved";
        }
        else
        {
            _className = "danger";
            _headingText = "Rejected";
        }
    }
    
    async Task ChangeApproval(bool approvalStatus)
    {
        await _leaveRequestService.ApproveLeaveRequest(id, approvalStatus);
        _navigationManager.NavigateTo("/leaverequests/");
    }
}