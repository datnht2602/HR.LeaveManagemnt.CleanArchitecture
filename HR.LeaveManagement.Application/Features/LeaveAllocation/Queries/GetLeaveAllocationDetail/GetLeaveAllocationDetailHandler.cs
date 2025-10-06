using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetail;

public class GetLeaveAllocationDetailHandler : IRequestHandler<GetLeaveAllocationDetailRequestQuery, LeaveAllocationDetailsDto>
{
    private readonly IMapper _mapper;
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly IAppLogger<GetLeaveAllocationDetailHandler> _logger;

    public GetLeaveAllocationDetailHandler(IMapper mapper, ILeaveAllocationRepository leaveAllocationRepository, IAppLogger<GetLeaveAllocationDetailHandler> logger)
    {
        _mapper = mapper;
        _leaveAllocationRepository = leaveAllocationRepository;
        _logger = logger;
    }
    
    public async Task<LeaveAllocationDetailsDto> Handle(GetLeaveAllocationDetailRequestQuery requestQuery, CancellationToken cancellationToken)
    {
        var leaveAllocation = await _leaveAllocationRepository.GetLeaveAllocationWithDetails(requestQuery.Id);
        return _mapper.Map<LeaveAllocationDetailsDto>(leaveAllocation);
    }
}