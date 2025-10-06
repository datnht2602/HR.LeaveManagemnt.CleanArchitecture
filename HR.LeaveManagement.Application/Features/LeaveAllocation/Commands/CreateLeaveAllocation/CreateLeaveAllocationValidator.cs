using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;

public class CreateLeaveAllocationValidator : AbstractValidator<CreateLeaveAllocationCommand>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public CreateLeaveAllocationValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;
        
        // RuleFor(q => q.EmployeeId)
        //     .NotNull().WithMessage("Employee Id is required");
        
        RuleFor(q => q.LeaveTypeId)
            .GreaterThan(0).WithMessage("Leave type Id must be greater than 0")
            .MustAsync(LeaveTypeMustExits)
            .NotNull().WithMessage("Leave type Id is required");
        
        // RuleFor(q => q.NumberOfDays)
        //     .LessThan(100).WithMessage("Number of days must not be less than 100")
        //     .GreaterThan(0).WithMessage("Number of days must be greater than 0")
        //     .NotNull().WithMessage("Number of days is required");
        //
        // RuleFor(q => q.Period)
        //     .LessThan(100).WithMessage("Number of days must not be less than 100")
        //     .GreaterThan(0).WithMessage("Number of days must be greater than 0")
        //     .NotNull().WithMessage("Period is required");
        //
    }

    private Task<bool> LeaveTypeMustExits(int id, CancellationToken arg2)
    {
        return _leaveTypeRepository.IsLeaveTypeExist(id);
    }
}