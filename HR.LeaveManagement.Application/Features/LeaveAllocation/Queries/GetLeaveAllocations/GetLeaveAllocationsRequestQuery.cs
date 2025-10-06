using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations;

public record GetLeaveAllocationsRequestQuery : IRequest<IEnumerable<LeaveAllocationDto>>;