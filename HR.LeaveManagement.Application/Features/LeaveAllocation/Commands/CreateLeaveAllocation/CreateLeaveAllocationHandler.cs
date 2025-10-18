using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;

public class CreateLeaveAllocationHandler : IRequestHandler<CreateLeaveAllocationCommand, Unit>
{
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IUserService _userService;

    public CreateLeaveAllocationHandler(ILeaveAllocationRepository leaveAllocationRepository, 
        ILeaveTypeRepository leaveTypeRepository, IUserService userService)
    {
        _leaveAllocationRepository = leaveAllocationRepository;
        _leaveTypeRepository = leaveTypeRepository;
        _userService = userService;
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

        var employees = await _userService.GetEmployees();
        var period = DateTime.Now.Year;
        List<Domain.LeaveAllocation> neededAddList = [];

        foreach (var employee in employees)
        {
            var allocationExist = await _leaveAllocationRepository.AllocationExists(command.LeaveTypeId, employee.Id, period);
            if (!allocationExist)
            {
                neededAddList.Add(new Domain.LeaveAllocation()
                {
                    EmployeeId = employee.Id,
                    LeaveTypeId = command.LeaveTypeId,
                    NumberOfDays = leaveType.DefaultDays,
                    Period = period
                });
            }
        }

        if (neededAddList.Count != 0)
        {
            await _leaveAllocationRepository.AddAllocations(neededAddList);
        }
        return Unit.Value;
    }
}