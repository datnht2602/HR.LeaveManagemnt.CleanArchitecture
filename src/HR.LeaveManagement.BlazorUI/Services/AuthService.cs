using Blazored.LocalStorage;
using HR.LeaveManagement.BlazorUI.Contracts;
using HR.LeaveManagement.BlazorUI.Provider;
using HR.LeaveManagement.BlazorUI.Services.Base;
using Microsoft.AspNetCore.Components.Authorization;

namespace HR.LeaveManagement.BlazorUI.Services;

public class AuthService : BaseHttpService, IAuthService
{
    private readonly AuthenticationStateProvider _authenticationStateProvider;

    public AuthService(IServiceClient serviceClient, ILocalStorageService localStorageService, AuthenticationStateProvider authenticationStateProvider) : base(serviceClient, localStorageService)
    {
        _authenticationStateProvider = authenticationStateProvider;
    }

    public async Task<bool> AuthenticatedAsync(string email, string password)
    {
        var authRequest = new AuthRequest()
        {
            Email = email,
            Password = password
        };
        try
        {
            var result = await ServiceClient.LoginAsync(authRequest);
            if (string.IsNullOrEmpty(result.Token))
            {
                return false;
            }
            await LocalStorageService.SetItemAsync("token", result.Token);
            await ((ApiAuthenticationStateProvider) _authenticationStateProvider).LoggedIn();
            return true;

        }
        catch (Exception e)
        {
            return false;
        }
    }

    public async Task LogoutAsync()
    {
        await ((ApiAuthenticationStateProvider) _authenticationStateProvider).LoggedOut();
    }

    public async Task<bool> RegisterAsync(string firstName, string lastName, string userName, string email, string password)
    {
        RegistrationRequest registrationRequest = new ()
        {
            FirstName = firstName,
            LastName = lastName,
            UserName = userName,
            Email = email,
            Password = password
        };
        var response = await ServiceClient.RegisterAsync(registrationRequest);
        return !string.IsNullOrEmpty(response.Token);
    }
}