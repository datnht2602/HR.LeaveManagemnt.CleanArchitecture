using System.Reflection;
using Blazored.LocalStorage;
using Blazored.Toast;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using HR.LeaveManagement.BlazorUI;
using HR.LeaveManagement.BlazorUI.Contracts;
using HR.LeaveManagement.BlazorUI.Handlers;
using HR.LeaveManagement.BlazorUI.Layout;
using HR.LeaveManagement.BlazorUI.Provider;
using HR.LeaveManagement.BlazorUI.Services;
using HR.LeaveManagement.BlazorUI.Services.Base;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddTransient<JwtAuthorizationMessageHandler>();
builder.Services.AddHttpClient<IServiceClient, ServiceClient>(client => client.BaseAddress = new Uri("https://localhost:7268"))
    .AddHttpMessageHandler<JwtAuthorizationMessageHandler>();

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddBlazoredToast();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();

builder.Services.AddScoped<ILeaveTypeService, LeaveTypeService>();
builder.Services.AddScoped<ILeaveAllocationService, LeaveAllocationService>();
builder.Services.AddScoped<ILeaveRequestService, LeaveRequestService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddAutoMapper(config =>
{
    config.LicenseKey = "eyJhbGciOiJSUzI1NiIsImtpZCI6Ikx1Y2t5UGVubnlTb2Z0d2FyZUxpY2Vuc2VLZXkvYmJiMTNhY2I1OTkwNGQ4OWI0Y2IxYzg1ZjA4OGNjZjkiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJodHRwczovL2x1Y2t5cGVubnlzb2Z0d2FyZS5jb20iLCJhdWQiOiJMdWNreVBlbm55U29mdHdhcmUiLCJleHAiOiIxNzg5OTQ4ODAwIiwiaWF0IjoiMTc1ODQzNjcxNyIsImFjY291bnRfaWQiOiIwMTk5NmFmZGM1YjA3NmNlYTNiOGM0OTkxYTdlNzhlNyIsImN1c3RvbWVyX2lkIjoiY3RtXzAxazVuZnh4MG1kZ2QwOXg4cDR4NHZ4djBqIiwic3ViX2lkIjoiLSIsImVkaXRpb24iOiIwIiwidHlwZSI6IjIifQ.wKD6jb4tPyiKem8eWsncFYYCc9izlD9zX8oOyooHGynErh9l_9yllnhFGycvhPID_2-MPxdBbaIBARYGOB36hvKy91OYRX4612LfcdxFV5qqhNJlF9C7PMuLIsKPWOnOkG8eXIMBaaNGsUCGYKJ4KkRwp7-OzC5tN5SURiXahJ5tM9jVjq4JEEHpIn_uvk1IaawfLurhMy0aqWgTXKworsVgLne5fV5vXJcAjubR3OZQGbazMAqPo0NpEO2AZKoeUd0s_QxmT2lLx7PNWBsl3h0gj43Pi18HIO86CfNiOUfvwCWQBV5vPLmPa4T9OvbXkrljdYJui6bWX49Uwn70eQ";
    config.AddMaps(Assembly.GetExecutingAssembly());
});

await builder.Build().RunAsync();