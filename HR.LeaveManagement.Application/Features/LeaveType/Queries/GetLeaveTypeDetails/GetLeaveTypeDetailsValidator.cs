using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;

public class GetLeaveTypeDetailsValidator : AbstractValidator<GetLeaveTypeDetailsQuery>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public GetLeaveTypeDetailsValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;

        RuleFor(q => q)
            .MustAsync(IsLeaveTypeExist)
            .WithMessage("Leave type not found");
    }

    private Task<bool> IsLeaveTypeExist(GetLeaveTypeDetailsQuery query, CancellationToken arg2)
    {
        return _leaveTypeRepository.IsLeaveTypeExist(query.Id);
    }
}