using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations;

public class GetLeaveAllocationsHandler : IRequestHandler<GetLeaveAllocationsRequestQuery, IEnumerable<LeaveAllocationDto>>
{
    private readonly IMapper _mapper;
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly IAppLogger<GetLeaveAllocationsHandler> _logger;

    public GetLeaveAllocationsHandler(IMapper mapper, ILeaveAllocationRepository leaveAllocationRepository, IAppLogger<GetLeaveAllocationsHandler> logger)
    {
        _mapper = mapper;
        _leaveAllocationRepository = leaveAllocationRepository;
        _logger = logger;
    }
    
    public async Task<IEnumerable<LeaveAllocationDto>> Handle(GetLeaveAllocationsRequestQuery requestQuery, CancellationToken cancellationToken)
    {
        var list = await _leaveAllocationRepository.GetLeaveAllocationsWithDetails();
        
        var allocations = _mapper.Map<IEnumerable<LeaveAllocationDto>>(list);
        
        return allocations;
    }
}