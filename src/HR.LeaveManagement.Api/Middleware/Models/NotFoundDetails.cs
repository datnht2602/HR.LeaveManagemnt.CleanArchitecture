using System.Net;
using HR.LeaveManagement.Application.Exceptions;

namespace HR.LeaveManagement.Api.Middleware.Models;

public class NotFoundDetails : CustomProblemDetails
{
    public NotFoundDetails(NotFoundException exception) : base(exception)
    {
        Type = nameof(NotFoundException);
        Status = (int) HttpStatusCode.NotFound;
    }
}