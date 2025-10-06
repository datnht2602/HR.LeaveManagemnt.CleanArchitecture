using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;

public record UpdateLeaveAllocationCommand(int Id, int LeaveTypeId, int NumberOfDays, int Period) : IRequest<Unit>;