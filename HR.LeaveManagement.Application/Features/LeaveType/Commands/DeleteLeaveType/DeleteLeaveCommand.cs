using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType;

public class DeleteLeaveCommand : IRequest<Unit>
{
    public int Id { get; set; }    
}