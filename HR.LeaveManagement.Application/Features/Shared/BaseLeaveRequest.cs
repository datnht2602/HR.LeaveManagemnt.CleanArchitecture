namespace HR.LeaveManagement.Application.Features.Shared;

public abstract class BaseLeaveRequest
{
    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }
    
    public int LeaveTypeId { get; set; }
}