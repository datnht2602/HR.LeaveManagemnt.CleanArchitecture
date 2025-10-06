using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagement.Api.Middleware.Models;

public abstract class CustomProblemDetails : ProblemDetails
{
    protected CustomProblemDetails(Exception exception)
    {
        Title = exception.Message;
        Detail = exception.InnerException?.Message;
    }
    protected IDictionary<string, string[]> Errors { get; set; } = new Dictionary<string, string[]>();
}