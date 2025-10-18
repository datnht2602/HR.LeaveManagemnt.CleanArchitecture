using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequests;

public class GetLeaveRequestsQuery : IRequest<List<LeaveRequestsDto>>
{
    public bool IsLoggedInUser { get; set; }
}