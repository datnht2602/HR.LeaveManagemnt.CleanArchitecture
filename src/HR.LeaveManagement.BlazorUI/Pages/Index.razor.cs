using System.Runtime.InteropServices;
using Blazored.LocalStorage;
using HR.LeaveManagement.BlazorUI.Contracts;
using HR.LeaveManagement.BlazorUI.Provider;
using HR.LeaveManagement.BlazorUI.Services.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace HR.LeaveManagement.BlazorUI.Pages;

public partial class Index
{
    [Inject] 
    public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
    
    [Inject] 
    public NavigationManager NavigationManager { get; set; }
    
    [Inject] 
    public IAuthService AuthService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await ((ApiAuthenticationStateProvider) AuthenticationStateProvider).GetAuthenticationStateAsync();
    }
    
    protected void GoToLogin()
    {
        NavigationManager.NavigateTo("login/");
    }

    protected void GoToRegister()
    {
        NavigationManager.NavigateTo("register/");
    }

    protected async void Logout()
    {
        await AuthService.LogoutAsync();
    }
}