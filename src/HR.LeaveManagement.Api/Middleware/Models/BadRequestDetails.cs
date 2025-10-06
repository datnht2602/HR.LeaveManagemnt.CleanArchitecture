
using System.Net;
using HR.LeaveManagement.Application.Exceptions;

namespace HR.LeaveManagement.Api.Middleware.Models;

public class BadRequestDetails : CustomProblemDetails
{
    public BadRequestDetails(BadRequestException badRequestException) : base(badRequestException)
    {
        Type = nameof(BadRequestException);
        Errors = badRequestException.ValidationErrors;
        Status = (int) HttpStatusCode.BadRequest;
    }


}