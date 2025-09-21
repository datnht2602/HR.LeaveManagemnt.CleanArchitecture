using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace HR.LeaveManagement.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection serviceCollection)
    {
        var licenseKey = "eyJhbGciOiJSUzI1NiIsImtpZCI6Ikx1Y2t5UGVubnlTb2Z0d2FyZUxpY2Vuc2VLZXkvYmJiMTNhY2I1OTkwNGQ4OWI0Y2IxYzg1ZjA4OGNjZjkiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJodHRwczovL2x1Y2t5cGVubnlzb2Z0d2FyZS5jb20iLCJhdWQiOiJMdWNreVBlbm55U29mdHdhcmUiLCJleHAiOiIxNzg5OTQ4ODAwIiwiaWF0IjoiMTc1ODQzNjcxNyIsImFjY291bnRfaWQiOiIwMTk5NmFmZGM1YjA3NmNlYTNiOGM0OTkxYTdlNzhlNyIsImN1c3RvbWVyX2lkIjoiY3RtXzAxazVuZnh4MG1kZ2QwOXg4cDR4NHZ4djBqIiwic3ViX2lkIjoiLSIsImVkaXRpb24iOiIwIiwidHlwZSI6IjIifQ.wKD6jb4tPyiKem8eWsncFYYCc9izlD9zX8oOyooHGynErh9l_9yllnhFGycvhPID_2-MPxdBbaIBARYGOB36hvKy91OYRX4612LfcdxFV5qqhNJlF9C7PMuLIsKPWOnOkG8eXIMBaaNGsUCGYKJ4KkRwp7-OzC5tN5SURiXahJ5tM9jVjq4JEEHpIn_uvk1IaawfLurhMy0aqWgTXKworsVgLne5fV5vXJcAjubR3OZQGbazMAqPo0NpEO2AZKoeUd0s_QxmT2lLx7PNWBsl3h0gj43Pi18HIO86CfNiOUfvwCWQBV5vPLmPa4T9OvbXkrljdYJui6bWX49Uwn70eQ";
        serviceCollection.AddAutoMapper(cfg =>
        {
            cfg.LicenseKey = licenseKey;
            cfg.AddMaps(Assembly.GetExecutingAssembly());
        });
        serviceCollection.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
            cfg.LicenseKey = licenseKey;
        });
        return serviceCollection;
    }
}