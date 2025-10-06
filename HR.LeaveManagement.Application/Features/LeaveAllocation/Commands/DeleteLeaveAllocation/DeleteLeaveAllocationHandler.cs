using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.DeleteLeaveAllocation;

public class DeleteLeaveAllocationHandler : IRequestHandler<DeleteLeaveAllocationCommand>
{
    private readonly IMapper _mapper;
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;

    public DeleteLeaveAllocationHandler(IMapper mapper, ILeaveAllocationRepository leaveAllocationRepository)
    {
        _mapper = mapper;
        _leaveAllocationRepository = leaveAllocationRepository;
    }
    
    public async Task Handle(DeleteLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        var leaveTypeToDelete = await _leaveAllocationRepository.GetByIdAsync(request.Id);

        if (leaveTypeToDelete == null)
        {
            throw new NotFoundException(nameof(Domain.LeaveType), request.Id);
        }

        await _leaveAllocationRepository.DeleteAsync(leaveTypeToDelete); 
    }
}