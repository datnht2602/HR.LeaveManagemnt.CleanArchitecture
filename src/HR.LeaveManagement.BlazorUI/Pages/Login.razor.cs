using HR.LeaveManagement.BlazorUI.Contracts;
using HR.LeaveManagement.BlazorUI.Models;
using Microsoft.AspNetCore.Components;

namespace HR.LeaveManagement.BlazorUI.Pages;

public partial class Login
{
    public LoginVM Model { get; set; }

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    public string Message { get; set; }

    [Inject]
    public IAuthService AuthService { get; set; }
    
    protected override void OnInitialized()
    {
        Model = new LoginVM();
    }

    protected async Task HandleLogin()
    {
        if (await AuthService.AuthenticatedAsync(Model.Email, Model.Password))
        {
            NavigationManager.NavigateTo("/");
        }
        Message = "Username/password combination unknown";
    }
}