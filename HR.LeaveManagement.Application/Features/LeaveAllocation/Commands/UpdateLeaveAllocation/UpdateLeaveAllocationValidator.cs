using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;

public class UpdateLeaveAllocationValidator : AbstractValidator<UpdateLeaveAllocationCommand>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;

    public UpdateLeaveAllocationValidator(ILeaveTypeRepository leaveTypeRepository, ILeaveAllocationRepository leaveAllocationRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;
        _leaveAllocationRepository = leaveAllocationRepository;

        RuleFor(p => p.NumberOfDays)
            .GreaterThan(0).WithMessage("Number of days must be greater than 0");
        
        RuleFor(q => q.LeaveTypeId)
            .GreaterThan(0).WithMessage("Leave type Id must be greater than 0")
            .MustAsync(LeaveTypeMustExits)
            .NotNull().WithMessage("Leave type Id is required");
        
        RuleFor(p => p.Id).NotNull().WithMessage("Id is required")
            .MustAsync(LeaveTypeAllocationMustExist).WithMessage("Leave type allocation not found");
    }

    private async Task<bool> LeaveTypeAllocationMustExist(int id, CancellationToken arg2)
    {
        return await _leaveAllocationRepository.GetByIdAsync(id) != null;
    }

    private Task<bool> LeaveTypeMustExits(int id, CancellationToken arg2)
    {
        return _leaveTypeRepository.IsLeaveTypeExist(id);
    }
}