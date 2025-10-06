using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;

public record CreateLeaveAllocationCommand(
    int LeaveTypeId,
    int NumberOfDays,
    string EmployeeId,
    int Period) : IRequest<Unit>;