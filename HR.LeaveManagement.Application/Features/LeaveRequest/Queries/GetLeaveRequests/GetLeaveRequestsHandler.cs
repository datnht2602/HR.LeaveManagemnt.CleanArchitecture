using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequests;

public class GetLeaveRequestsHandler : IRequestHandler<GetLeaveRequestsQuery, List<LeaveRequestsDto>>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;

    public GetLeaveRequestsHandler(ILeaveRequestRepository leaveRequestRepository,
        IMapper mapper,
        IUserService userService)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _mapper = mapper;
        _userService = userService;
    }
    
    public async Task<List<LeaveRequestsDto>> Handle(GetLeaveRequestsQuery request, CancellationToken cancellationToken)
    {
        // Check if it is logged in employee

        var leaveRequests = await _leaveRequestRepository.GetLeaveRequestsWithDetails();
        var requests = _mapper.Map<List<LeaveRequestsDto>>(leaveRequests);

        if (request.IsLoggedInUser)
        {
            var userId = _userService.UserId;
            leaveRequests = await _leaveRequestRepository.GetLeaveRequestsWithDetails(userId);

            var employee = await _userService.GetEmployeeById(userId);
            requests = _mapper.Map<List<LeaveRequestsDto>>(leaveRequests);
            foreach (var req in requests)
            {
                req.Employee = employee;
            }
        }
        else
        {
            leaveRequests = await _leaveRequestRepository.GetLeaveRequestsWithDetails();
            requests = _mapper.Map<List<LeaveRequestsDto>>(leaveRequests);
            foreach (var req in requests)
            {
                req.Employee = await _userService.GetEmployeeById(req.RequestingEmployeeId);
            }
        }

        // Fill requests with employee information

        return requests;
    }
}