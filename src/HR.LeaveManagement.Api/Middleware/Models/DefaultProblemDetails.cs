using System.Net;

namespace HR.LeaveManagement.Api.Middleware.Models;

public class DefaultProblemDetails : CustomProblemDetails
{
    public DefaultProblemDetails(Exception exception) : base(exception)
    {
        Title = exception.Message;
        Detail = exception.StackTrace;
        Type = nameof(Exception);
        Status = (int) HttpStatusCode.InternalServerError;
    }

}