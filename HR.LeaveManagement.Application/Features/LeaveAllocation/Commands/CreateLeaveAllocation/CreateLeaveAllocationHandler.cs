using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;

public class CreateLeaveAllocationHandler : IRequestHandler<CreateLeaveAllocationCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IAppLogger<CreateLeaveAllocationHandler> _logger;

    public CreateLeaveAllocationHandler(IMapper mapper, ILeaveAllocationRepository leaveAllocationRepository, ILeaveTypeRepository leaveTypeRepository, IAppLogger<CreateLeaveAllocationHandler> logger)
    {
        _mapper = mapper;
        _leaveAllocationRepository = leaveAllocationRepository;
        _leaveTypeRepository = leaveTypeRepository;
        _logger = logger;
    }
    public async Task<Unit> Handle(CreateLeaveAllocationCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateLeaveAllocationValidator(_leaveTypeRepository);
        var validationResult = await validator.ValidateAsync(command);

        if (validationResult.Errors.Any())
        {
            throw new BadRequestException("Invalid Leave Allocation Request", validationResult);
        }

        var leaveType = await _leaveTypeRepository.GetByIdAsync(command.LeaveTypeId);

        var leaveAllocation = _mapper.Map<Domain.LeaveAllocation>(command);
        await _leaveAllocationRepository.AddAsync(leaveAllocation);
        return Unit.Value;
    }
}