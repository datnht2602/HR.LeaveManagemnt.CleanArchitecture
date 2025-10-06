using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType;

public record DeleteLeaveCommand(int Id) : IRequest<Unit>;