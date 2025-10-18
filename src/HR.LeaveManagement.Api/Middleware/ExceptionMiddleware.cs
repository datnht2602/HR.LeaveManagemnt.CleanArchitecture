using System.Net;
using HR.LeaveManagement.Api.Middleware.Models;
using HR.LeaveManagement.Application.Exceptions;

namespace HR.LeaveManagement.Api.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        CustomProblemDetails problem = exception switch 
        {
            BadRequestException badRequestException => new BadRequestDetails(badRequestException),
            NotFoundException notFoundException => new NotFoundDetails(notFoundException),
            _ => new DefaultProblemDetails(exception)
        };
        context.Response.StatusCode = problem.Status ?? (int) HttpStatusCode.InternalServerError;
        _logger.LogError(exception, "An unhandled exception occurred");
        await context.Response.WriteAsJsonAsync(problem);
    }
}