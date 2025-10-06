using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetail;

public record GetLeaveAllocationDetailRequestQuery(int Id) : IRequest<LeaveAllocationDetailsDto>;